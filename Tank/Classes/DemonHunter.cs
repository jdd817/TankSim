using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Tank.Abilities;
using Tank.Buffs;
using Tank.DataLogging;

namespace Tank.Classes
{
    public class DemonHunter:Player
    {
        /*
        Currently used talents:
            N/A
            Feast of Souls

        TBI:
            Felblade
            Feed the Demon
            N/A
            Fel Devestation
            N/A except for Soul Barrier, which is not viable
        */

        public DemonHunter(IBuffManager buffManager, ICooldownManager cooldownManager, IAbilityManager abilityManager, IRng rng)
            : base(buffManager, cooldownManager, abilityManager, rng)
        {
            V = 0.0315m;
            BaseDodge = 0.015m;
            BaseParry = 0.03m;

            PainCap = 100;

            UsesTwoHanders = false;

            AldrachiWarblades = new Artifacts.DemonHunter(rng)
            {
                SoulCarver = 1,
                TormentedSouls = 1,
                ShatterTheSouls = 4,
                SiphonPower = 1,
                DefensiveSpikes = 1,
                Painbringer = 1,
                AldrachiDesign = 4,
                FieryDemise = 4,
                FueledByPain = 1,
                EmbraceThePain = 4,
                DemonicFlames = 1,
                HonedWarblades = 4,
                WillOfTheIllidari = 4,
                AuraOfPain = 4,
                CharredWarblades = 1,
                DevourSouls = 4,
                InternalForce = 4,
                Soulgorger = 1,
                EruptingSouls = 1,
                LingeringOrdeal = 4,
                FlamingSoul = 1,
                Concordance = 6
            };

            foreach (var buff in AldrachiWarblades.GetArtifactBuffs())
                Buffs.AddBuff(buff);

            Reset();
        }

        public Artifacts.DemonHunter AldrachiWarblades { get; set; }

        public override void Reset()
        {
            Buffs.ClearAllNonPermanent();
            Cooldowns.Reset();
            Cooldowns.GCDLength = GCDLength;

            Pain = 0;
            CurrentHealth = MaxHealth;

            ShearsSinceLastSoulFragment = 0;
        }

        private int _pain;

        public int Pain
        {
            get { return _pain; }
            set { _pain = Math.Min(value, PainCap); }
        }

        public int PainCap
        { get; set; }

        #region class-specific counters

        internal int ShearsSinceLastSoulFragment;

        #endregion

        #region class-specific calculated values
        
        public override int AttackPower
        {
            get
            {
                return (int)((
                    Agility
                    + Buffs.GetRatingAdjustment(StatType.AttackPower))
                    * (1m + Mastery * (2m / 3m))
                    * (1m + Buffs.GetPercentageAdjustment(StatType.AttackPower)));
            }
        }
        
        public override decimal CritChance
        { get { return 0.15m + RatingConverter.GetRating(StatType.Crit, CritRating); } }

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

        public override decimal ParryChance
        {
            get
            {
                return BaseParry
                    + GetDiminishedAvoidance(
                        RatingConverter.GetRating(StatType.Parry, CritRating + Buffs.GetRatingAdjustment(StatType.Parry))
                        + Buffs.GetPercentageAdjustment(StatType.Parry));
            }
        }

        public override decimal Haste
        { get { return .012m + RatingConverter.GetRating(StatType.Haste, HasteRating) + Buffs.GetPercentageAdjustment(StatType.Haste); } }

        #endregion 


        public override Abilities.Ability GetAbilityUsed(IBuffManager MobBuffs)
        {
            if (Cooldowns.OffGCD)
            {
                if (Cooldowns.AbilityReady<Abilities.DemonHunter.SoulCarver>())
                    return AbilityManger.GetAbility<Abilities.DemonHunter.SoulCarver>();

                //if (Pain >= 30 && Cooldowns.AbilityReady<Abilities.DemonHunter.FelDevestation>() && HealthPercentage <= 0.75m)
                //    return AbilityManger.GetAbility<Abilities.DemonHunter.FelDevestation>();

                if (Buffs.GetBuff<Talents.DemonHunter.Fracture>() != null)
                {
                    var soulFragments = Buffs.GetStacks<Buffs.DemonHunter.LesserSoulFragment>();
                    if (soulFragments < 3 && Pain >= 30)
                        return AbilityManger.GetAbility<Abilities.DemonHunter.Fracture>();
                }

                if (Pain >= 70)
                    return AbilityManger.GetAbility<Abilities.DemonHunter.SoulCleave>();
                
                if (Cooldowns.AbilityReady<Abilities.DemonHunter.ImmolationAura>())
                    return AbilityManger.GetAbility<Abilities.DemonHunter.ImmolationAura>();

                if (Buffs.GetBuff<Talents.DemonHunter.Felblade>() != null && Cooldowns.AbilityReady<Abilities.DemonHunter.FelBlade>())
                    return AbilityManger.GetAbility<Abilities.DemonHunter.FelBlade>();

                if (Cooldowns.AbilityReady<Abilities.DemonHunter.SigilOfFlame>())
                    return AbilityManger.GetAbility<Abilities.DemonHunter.SigilOfFlame>();
                
                if (Pain >= 60 && HealthPercentage <= 0.75m)
                    return AbilityManger.GetAbility<Abilities.DemonHunter.SoulCleave>();

                return AbilityManger.GetAbility<Abilities.DemonHunter.Shear>();
            }

            if (Cooldowns.AbilityReady<Abilities.DemonHunter.DemonSpikes>() && Pain >= 20 && Buffs.GetBuff(typeof(Buffs.DemonHunter.DemonSpikes)) == null)
                return AbilityManger.GetAbility<Abilities.DemonHunter.DemonSpikes>();

            if (Cooldowns.AbilityReady<Abilities.DemonHunter.Metamorphosis>() && Buffs.GetBuff<Buffs.DemonHunter.FieryBrand>() == null && Buffs.GetBuff<Buffs.DemonHunter.Metamorphosis>() == null)
                return AbilityManger.GetAbility<Abilities.DemonHunter.Metamorphosis>();

            if (Cooldowns.AbilityReady<Abilities.DemonHunter.FieryBrand>() && Buffs.GetBuff<Buffs.DemonHunter.Metamorphosis>() == null)
                return AbilityManger.GetAbility<Abilities.DemonHunter.FieryBrand>();

            return null;
        }

        public override void UpdateAbilityResults(decimal CurrentTime, Abilities.Ability Ability, AbilityResult Result)
        {
            Pain -= Result.ResourceCost;
            //ApplyHealing(Result.SelfHealing);
        }

        public override DamageEvent UpdateFromMobAttack(DamageEvent DamageEvent)
        {
            //get pain
            //absent finding a blue post regarding this, using the same formula as warrior rage
            Pain += (int)((50m * DamageEvent.RawDamage) / MaxHealth);

            return DamageEvent;
        }

        public override void UpdateFromTickingBuffs(IEnumerable<Buffs.Buff> TickingBuffs)
        {
            base.UpdateFromTickingBuffs(TickingBuffs);
            foreach (var aura in TickingBuffs.OfType<Buffs.DemonHunter.ImmolationAura>())
                Pain += 2;
        }
    }
}
