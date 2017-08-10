using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Tank.Buffs.Warrior;
using System.Xml.Serialization;
using Tank.Abilities;

namespace Tank.Classes
{
    public class Druid : Player
    {
        public Druid(IBuffManager buffManager, ICooldownManager cooldownManager, IAbilityManager abilityManager, IRng rng)
            : base(buffManager, cooldownManager, abilityManager, rng)
        {
            BaseDodge = 0.10m;
            BaseParry = 0.00m;
            BaseBlock = 0.00m;

            UsesTwoHanders = false;

            RageCap = 100;

            Buffs.AddBuff(new Buffs.Druid.BearForm());

            ClawsOfUrsoc = new Artifacts.Druid(rng)
            {
                RageOfTheSleeper = 1,
                IronClaws = 1,
                ViciousBites = 4,
                Wildflesh = 4,
                JaggedClaws = 4,
                ReinforcedFur = 4,
                AdaptiveFur = 1,
                BearHug = 1,
                PerpetualSpring = 4,
                EmbraceOfTheNightmare = 1,
                SharpenedInstincts = 4,
                RoarOfTheCrowd = 1,
                Mauler = 4,
                UrsocsEndurance = 4,
                GoryFur = 1,
                BestialFortitude = 4,
                BloodyPaws = 1,
                FortitudeOfTheCenarionCircle = 1,
                ScintillatingMoonlight = 4,
                Fleshknitting = 1,
                PawsitiveOutlook = 1,
                Concordance = 6
            };

            foreach (var buff in ClawsOfUrsoc.GetArtifactBuffs())
                Buffs.AddBuff(buff);

            Reset();
        }

        public Artifacts.Druid ClawsOfUrsoc { get; set; }

        public override void Reset()
        {
            Buffs.ClearAllNonPermanent();
            Cooldowns.Reset();
            Cooldowns.GCDLength = GCDLength;

            Rage = 0;

            YserasGiftCD = 0m;

            CurrentHealth = MaxHealth;
        }

        [XmlIgnore]
        public override decimal ParryChance
        {
            get
            {
                return 0;
            }
        }

        

        private int _rage;

        public int Rage
        {
            get { return _rage; }
            set { _rage = Math.Min(value, RageCap); }
        }

        public int RageCap
        { get; set; }

        #region class-specific counters
        
        private decimal YserasGiftCD;

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

        [XmlIgnore]
        public override decimal CritChance
        { get { return 0.16m + RatingConverter.GetRating(StatType.Crit, CritRating); } }

        [XmlIgnore]
        public override decimal DodgeChance
        {
            get
            {
                return BaseDodge
                    + GetDiminishedAvoidance(
                        RatingConverter.GetRating(StatType.Dodge, CritRating + Buffs.GetRatingAdjustment(StatType.Dodge))
                            + Buffs.GetPercentageAdjustment(StatType.Dodge));
            }
        }

        public double GoreChance
        {
            get
            {
                return Buffs.BuffActive<Buffs.Druid.Artifact.BearHug>() ? 0.20 : 0.15;
            }
        }

        #endregion 


        public override Abilities.Ability GetAbilityUsed(IBuffManager MobBuffs)
        {
            if (Cooldowns.OffGCD)
            {
                if (Buffs.GetBuff<Talents.Druid.GalacticGuardian_Buff>() != null)
                    return AbilityManger.GetAbility<Abilities.Druid.Moonfire>();
                if (Cooldowns.AbilityReady<Abilities.Druid.Mangle>())
                    return AbilityManger.GetAbility<Abilities.Druid.Mangle>();
                if (Cooldowns.AbilityReady<Abilities.Druid.Thrash>() && Rage >= 15)
                    return AbilityManger.GetAbility<Abilities.Druid.Thrash>();
                var moonfireDebuff = MobBuffs.GetBuff<Buffs.Druid.Moonfire>();
                if (moonfireDebuff == null || moonfireDebuff.TimeRemaining <= GCDLength * 2m)
                {
                    return AbilityManger.GetAbility<Abilities.Druid.Moonfire>();
                }
                if (Rage >= RageCap - 30)
                    return AbilityManger.GetAbility<Abilities.Druid.Maul>();
                return AbilityManger.GetAbility<Abilities.Druid.Swipe>();

            }

            if (Buffs.BuffActive<Talents.Druid.BristlingFur>() && Cooldowns.AbilityReady<Abilities.Druid.BristlingFur>())
                return AbilityManger.GetAbility<Abilities.Druid.BristlingFur>();

            if (Rage >= 10 && Cooldowns.AbilityReady<Abilities.Druid.FrenziedRegeneration>() && HealthPercentage < 0.5m && Buffs.GetBuff<Buffs.Druid.FrenziedRegeneration>() == null)
                return AbilityManger.GetAbility<Abilities.Druid.FrenziedRegeneration>();

            if (Rage >= 45 && Cooldowns.AbilityReady<Abilities.Druid.Ironfur>())
                return AbilityManger.GetAbility<Abilities.Druid.Ironfur>();

            if (Rage >= 10 && Cooldowns.AbilityReady<Abilities.Druid.FrenziedRegeneration>() && Abilities.Druid.FrenziedRegeneration.HealAmount + CurrentHealth <= MaxHealth)
                return AbilityManger.GetAbility<Abilities.Druid.FrenziedRegeneration>();

            return null;
        }

        public override void UpdateAbilityResults(decimal CurrentTime, Abilities.Ability Ability, AbilityResult Result)
        {
            //Result.SelfHealing = ApplyHealing(Result.SelfHealing);
            Rage -= Result.ResourceCost;
            if (Ability.GetType() == typeof(Abilities.Attack))
                Rage += 8;
        }

        public override DataLogging.DamageEvent UpdateFromMobAttack(DataLogging.DamageEvent DamageEvent)
        {
            return DamageEvent;
        }

        public override int ApplyHealing(int healingAmount)
        {
            return base.ApplyHealing((int)(healingAmount * (1 + Mastery * 1.25m)));
        }
    }
}
