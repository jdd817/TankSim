using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.DataLogging;

namespace Tank.Classes
{
    public class Monk:Player
    {
        public Monk(IBuffManager buffManager, ICooldownManager cooldownManager, IAbilityManager abilityManager, IRng rng)
            : base(buffManager, cooldownManager, abilityManager, rng)
        {
            V = 0.0315m;
            BaseDodge = 0.10m;
            BaseParry = 0.03m;

            EnergyCap = 100;

            UsesTwoHanders = true;

            Buffs.AddBuff(new Buffs.Monk.GiftOfTheOxAura(rng));
            Buffs.AddBuff(new Buffs.Monk.StaggerAura());
            Buffs.AddBuff(new Buffs.Monk.HealingElixerAura());
            Buffs.AddBuff(new Buffs.Monk.CelestialFortuneAura(rng));

            Reset();
        }

        public override void Reset()
        {
            Buffs.ClearAllNonPermanent();
            Cooldowns.Reset();
            Cooldowns.GCDLength = GCDLength;

            Energy = 0;
            CurrentHealth = MaxHealth;
        }

        private int _energy;

        private int Energy
        {
            get { return _energy; }
            set { _energy = Math.Min(value, EnergyCap); }
        }

        public int EnergyCap
        { get; set; }

        public override decimal DodgeChance
        {
            get
            {
                return BaseDodge
                    + GetDiminishedAvoidance(
                        RatingConverter.GetRating(StatType.Dodge, Agility + Buffs.GetRatingAdjustment(StatType.Dodge))
                            + Buffs.GetPercentageAdjustment(StatType.Dodge));
            }
        }

        public override decimal CritChance
        { get { return 0.15m + RatingConverter.GetRating(StatType.Crit, CritRating); } }

        #region class-specific counters

        #endregion

        #region class-specific calculated values

        public override int AttackPower
        {
            get
            {
                return (int)((
                    Agility
                    + Buffs.GetRatingAdjustment(StatType.AttackPower))
                    * (1 + Buffs.GetPercentageAdjustment(StatType.AttackPower)));
            }
        }

        #endregion 


        public override Abilities.Ability GetAbilityUsed(IBuffManager MobBuffs)
        {
            if (Cooldowns.OffGCD)
            {
                if (Cooldowns.AbilityReady<Abilities.Monk.KegSmash>())
                    return AbilityManger.GetAbility<Abilities.Monk.KegSmash>();
                if (Energy >= 65)
                    return AbilityManger.GetAbility<Abilities.Monk.TigerPalm>();
                if (Cooldowns.AbilityReady<Abilities.Monk.BlackoutStrike>())
                    return AbilityManger.GetAbility<Abilities.Monk.BlackoutStrike>();
            }

            if (HealthPercentage <= 0.85m && Cooldowns.ChargesAvailable<Abilities.Monk.HealingElixer>() == 2)
                return AbilityManger.GetAbility<Abilities.Monk.HealingElixer>();

            if (CurrentHealth < MaxHealth * 0.35
                    && Cooldowns.ChargesAvailable<Abilities.Monk.IronskinBrew>() >= 2
                    && Buffs.GetBuff(typeof(Buffs.Monk.IronskinBrew)) == null)
                return AbilityManger.GetAbility<Abilities.Monk.IronskinBrew>();


            //both ironskin and purifying brews are tracked under ironskin
            var stagger = Buffs.GetBuff(typeof(Buffs.Monk.Stagger)) as Buffs.Monk.Stagger;
            if (stagger != null && stagger.DamageDelayed >= MaxHealth * 0.20m && Cooldowns.AbilityReady<Abilities.Monk.IronskinBrew>())
                return AbilityManger.GetAbility<Abilities.Monk.PurifyingBrew>();

            if (stagger != null && Cooldowns.ChargesAvailable<Abilities.Monk.IronskinBrew>() == 3 && stagger.DamageDelayed >= MaxHealth * 0.10m)
                return AbilityManger.GetAbility<Abilities.Monk.PurifyingBrew>();

            if (Cooldowns.ChargesAvailable<Abilities.Monk.IronskinBrew>() == 3)
                return AbilityManger.GetAbility<Abilities.Monk.IronskinBrew>();

            return null;
        }

        public override void UpdateAbilityResults(decimal CurrentTime, Abilities.Ability Ability, AbilityResult Result)
        {
            Energy -= Result.ResourceCost;
        }

        public override void UpdateTimeElapsed(decimal DeltaTime)
        {
            base.UpdateTimeElapsed(DeltaTime);

            var healingOrbs = Buffs.GetBuff<Buffs.Monk.HealingOrb>();
            if (healingOrbs != null)
            {
                bool absorbOrbs = false;
                if (healingOrbs.Stacks >= 3)
                    absorbOrbs = true;
                if (HealthPercentage <= 0.50m)
                    absorbOrbs = true;
                if (healingOrbs.TimeRemaining <= 0.25m)
                    absorbOrbs = true;

                if(absorbOrbs)
                {
                    var healingAmount = ApplyHealing((int)(healingOrbs.Stacks * AttackPower * 7.5m));
                    Buffs.ClearBuff<Buffs.Monk.HealingOrb>();
                    DataLogging.DataLogManager.LogHeal(new HealingEvent
                    {
                        Time = DataLogging.DataLogManager.CurrentTime,
                        Name = "GiftOfTheOx",
                        Amount = healingAmount
                    });
                }
            }
        }

        public override DamageEvent UpdateFromMobAttack(DamageEvent DamageEvent)
        {
            return DamageEvent;
        }
        
        public override int ApplyHealing(int healingAmount)
        {
            CurrentHealth += healingAmount;

            return healingAmount;
        }
    }
}
