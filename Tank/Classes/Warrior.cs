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
        static Warrior()
        {
            BaseDodge = 0.03m;
            BaseParry = 0.10m;
            BaseBlock = 0.30m;
        }

        public Warrior()
        {
            Reset();
        }

        public override void Reset()
        {
            Buffs.ClearAllNonPermanent();

            Rage = 20;
            RageCounter = 0;

            ShieldBlockRecharge = 0;
            ShieldBlockCharges = 2;
            ShieldSlamCD = 0;
            RevengeCD = 0;
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

        private decimal RageCounter;
        private decimal ShieldBlockRecharge;
        private int ShieldBlockCharges;
        private decimal ShieldSlamCD;
        private decimal RevengeCD;
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


        public override Abilities.Ability GetAbilityUsed(BuffManager MobBuffs)
        {
            if (GCD <= 0)
            {
                if (ShieldSlamCD <= 0)
                {
                    ShieldSlamCD = 9.0m;
                    if (Buffs.GetBuff(typeof(Buffs.Warrior.SwordAndBoard)) != null)
                        Buffs.ClearBuff(typeof(Buffs.Warrior.SwordAndBoard));
                    return new Abilities.Warrior.ShieldSlam();
                }
                if (RevengeCD <= 0)
                {
                    RevengeCD = 9.0m;
                    return new Abilities.Warrior.Revenge();
                }
                return new Abilities.Warrior.Devastate();
            }

            if (ShieldBlockCharges > 0 && Rage >= 10 && Buffs.GetBuff(typeof(Buffs.Warrior.ShieldBlock)) == null)
            {
                ShieldBlockCharges--;
                if (ShieldBlockCharges == 0)
                    ShieldBlockRecharge = 13;
                return new Abilities.Warrior.ShieldBlock();
            }
            if (Rage >= 60 && Buffs.GetBuff(typeof(IgnorePain)) == null)
            {
                return new Abilities.Warrior.IgnorePain()
                {
                    ResourceCost = 60,
                    DamageAbsorbed = (int)(28.0m * AttackPower * (1.75m - 0.75m * CurrentHealth / MaxHealth))
                };
            }

            return null;
        }

        public override void UpdateAbilityResults(decimal CurrentTime, Abilities.Ability Ability, AbilityResult Result)
        {
            Rage -= Result.ResourceCost;
            if(Ability.GetType()==typeof(Abilities.Warrior.ShieldSlam))
            {
                var shieldBlock = Buffs.GetBuff(typeof(ShieldBlock));
                if (shieldBlock != null)
                    shieldBlock.TimeRemaining += 1.5m;
            }
        }

        public override void UpdateTimeElapsed(decimal DeltaTime)
        {
            UpdateTimers(DeltaTime);
            RageCounter += DeltaTime;
            /*if (RageCounter >= 3)
            {
                RageCounter = 0;
                Rage++;
            }*/

            if (ShieldBlockCharges < 2)
            {
                ShieldBlockRecharge -= DeltaTime;
                if (ShieldBlockRecharge <= 0)
                {
                    ShieldBlockCharges++;
                    if (ShieldBlockCharges < 2)
                        ShieldBlockRecharge += 13;
                }
            }

            ShieldSlamCD = Math.Max(0, ShieldSlamCD - DeltaTime);
            RevengeCD = Math.Max(0, RevengeCD - DeltaTime);
            RevengeResetICD = Math.Max(0, RevengeResetICD - DeltaTime);
            if (Buffs.GetBuff(typeof(SwordAndBoard)) != null)
            {
                ShieldSlamCD = 0;
            }
        }

        public override void UpdateFromMobAttack(decimal CurrentTime, Abilities.Attack MobAttack, AttackResult Result)
        {
            DataLogging.DamageEvent DamageEvent = new DataLogging.DamageEvent()
            {
                Time = CurrentTime,
                Result = Result,
                DamageTaken = MobAttack.Damage
            };

            if (Result == AttackResult.Dodge || Result == AttackResult.Parry)
            {
                if (RevengeResetICD <= 0 && RevengeCD>0)
                {
                    RevengeCD = 0;
                    RevengeResetICD = 3.0m;
                }
                DamageEvent.DamageTaken = 0;
            }

            DamageEvent.DamageTaken = (int)(DamageEvent.DamageTaken * (1m - VersatilityDamageReduction));

            //get rage
            //from blue: 50 * DamageTaken / MaxHealth
            Rage += (int)((50m * DamageEvent.DamageTaken) / MaxHealth);

            if (Result == AttackResult.Block)
            {
                if (RNG.NextDouble() < (double)CritBlockChance)
                    DamageEvent.DamageTaken = (int)(MobAttack.Damage * 0.40);
                else
                    DamageEvent.DamageTaken = (int)(MobAttack.Damage * 0.70);
                DamageEvent.DamageBlocked = MobAttack.Damage - DamageEvent.DamageTaken;
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

            CurrentHealth -= DamageEvent.DamageTaken;

            DataLogging.DataLogManager.LogEvent(DamageEvent);
        }

        public override void UpdateFromTickingBuffs(IEnumerable<Buffs.Buff> TickingBuffs)
        { }
    }
}
