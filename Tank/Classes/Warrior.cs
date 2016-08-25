using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Tank.Buffs.Warrior;
using System.Xml.Serialization;
using Tank.Abilities;

namespace Tank.Classes
{
    public class Warrior : Player
    {
        public Warrior(IBuffManager buffManager, ICooldownManager cooldownManager, IAbilityManager abilityManager, IRng rng)
            : base(buffManager, cooldownManager, abilityManager, rng)
        {
            BaseDodge = 0.03m;
            BaseParry = 0.10m;
            BaseBlock = 0.30m;

            RageCap = 120;

            Reset();
        }

        public override void Reset()
        {
            Buffs.ClearAllNonPermanent();
            Cooldowns.Reset();
            Cooldowns.GCDLength = GCDLength;

            Rage = 0;
            
            RevengeResetICD = 0;
            CurrentHealth = MaxHealth;

        }

        [XmlIgnore]
        public override decimal ParryChance
        {
            get
            {
                return BaseParry
                    + GetDiminishedAvoidance(
                        RatingConverter.GetRating(StatType.Parry, (int)(Strength / 3.88m) + Buffs.GetRatingAdjustment(StatType.Parry))
                        + Buffs.GetPercentageAdjustment(StatType.Parry));
            }
        }

        private int _rage;

        private int Rage
        {
            get { return _rage; }
            set { _rage = Math.Min(value, RageCap); }
        }

        public int RageCap
        { get; set; }

        #region class-specific counters
        
        private decimal RevengeResetICD;

        #endregion

        #region class-specific calculated values

        [XmlIgnore]
        public decimal CritBlockChance
        { get { return 0.12m + RatingConverter.GetRating(StatType.Mastery, MasteryRating); } }

        public override int AttackPower
        {
            get 
            {
                return (int)((
                    Strength 
                    + Buffs.GetRatingAdjustment(StatType.AttackPower))
                    * (1 + Buffs.GetPercentageAdjustment(StatType.AttackPower)));
            }
        }

        public override decimal BlockChance
        {
            get
            {
                return Mastery / 3m
                    + GetDiminishedAvoidance(
                        Buffs.GetPercentageAdjustment(StatType.Block));
            }
        }

        #endregion 


        public override Abilities.Ability GetAbilityUsed(IBuffManager MobBuffs)
        {
            if (Cooldowns.OffGCD)
            {
                if (Cooldowns.AbilityReady<Abilities.Warrior.ShieldSlam>())
                    return AbilityManger.GetAbility<Abilities.Warrior.ShieldSlam>();
                if (Cooldowns.AbilityReady<Abilities.Warrior.Revenge>())
                    return AbilityManger.GetAbility<Abilities.Warrior.Revenge>();
                return AbilityManger.GetAbility<Abilities.Warrior.Devastate>();
            }

            if (Cooldowns.AbilityReady<Abilities.Warrior.ShieldBlock>() && Rage >= 10 && Buffs.GetBuff(typeof(Buffs.Warrior.ShieldBlock)) == null)
                return AbilityManger.GetAbility<Abilities.Warrior.ShieldBlock>();

            if (Rage >= 60 && Buffs.GetBuff(typeof(IgnorePain)) == null)
                return AbilityManger.GetAbility<Abilities.Warrior.IgnorePain>();

            return null;
        }

        public override void UpdateAbilityResults(decimal CurrentTime, Abilities.Ability Ability, AbilityResult Result)
        {
            Rage -= Result.ResourceCost;
            ApplyHealing(Result.SelfHealing);
            if (Ability.GetType()==typeof(Abilities.Warrior.ShieldSlam))
            {
                var shieldBlock = Buffs.GetBuff(typeof(ShieldBlock));
                if (shieldBlock != null)
                    shieldBlock.TimeRemaining += 1.5m;
            }
        }

        public override void UpdateTimeElapsed(decimal DeltaTime)
        {
            base.UpdateTimeElapsed(DeltaTime);
            RevengeResetICD = Math.Max(0, RevengeResetICD - DeltaTime);
        }

        public override DataLogging.DamageEvent UpdateFromMobAttack(DataLogging.DamageEvent DamageEvent)
        {
            if (DamageEvent.Result == AttackResult.Dodge || DamageEvent.Result == AttackResult.Parry)
            {
                if (RevengeResetICD <= 0 && !Cooldowns.AbilityReady<Abilities.Warrior.Revenge>())
                {
                    Cooldowns.ReduceTimers(new CooldownReduction { Ability = typeof(Abilities.Warrior.Revenge), Amount = 0, ReductionType = ReductionType.By });
                    RevengeResetICD = 3.0m;
                }
            }

            //get rage
            //from blue: 50 * DamageTaken / MaxHealth
            Rage += (int)((50m * DamageEvent.RawDamage) / MaxHealth);

            if (DamageEvent.Result == AttackResult.Block)
            {
                if (Rng.NextDouble() < (double)CritBlockChance)
                    DamageEvent.DamageTaken = (int)(DamageEvent.DamageTaken * 0.40);
                else
                    DamageEvent.DamageTaken = (int)(DamageEvent.DamageTaken * 0.70);
                DamageEvent.DamageBlocked = DamageEvent.DamageTaken - DamageEvent.DamageTaken;
            }

            IgnorePain Barrier = (IgnorePain)Buffs.GetBuff(typeof(IgnorePain));
            if (Barrier != null)
            {
                var BarrierHit = Math.Min(Barrier.DamageRemaining, DamageEvent.DamageTaken);
                int Absorbed = (int)(BarrierHit * 0.90m);
                DamageEvent.DamageTaken = DamageEvent.DamageTaken - Absorbed;
                DamageEvent.DamageAbsorbed = Absorbed;
                Barrier.DamageRemaining -= BarrierHit;
            }

            return DamageEvent;
        }
    }
}
