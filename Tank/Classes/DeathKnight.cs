using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Tank.Abilities;

using Tank.Buffs.DeathKnight;
using Artifact = Tank.Buffs.DeathKnight.Artifact;

namespace Tank.Classes
{
    public class DeathKnight : Player
    {

        public DeathKnight(IBuffManager buffManager, ICooldownManager cooldownManager, IAbilityManager abilityManager, IRng rng)
            : base(buffManager, cooldownManager, abilityManager, rng)
        {
            BaseDodge = 0.03m;
            BaseParry = 0.06m;
            BaseBlock = 0.00m;

            RunicPowerCap = 125;

            UsesTwoHanders = true;

            Buffs.AddBuff(new DeathKnightMastery());

            MawOfTheDamned = new Artifacts.DeathKnight(rng)
            {
                Consumption = 1,
                SanguinaryAffinity = 1,
                VampiricFangs = 4,
                RattlingBones = 1,
                Bonebreaker = 4,
                AllConsumingRot = 4,
                UnendingThirst = 1,
                GrimPerseverance = 4,
                SkeletalShattering = 1,
                BloodFeast = 1,
                IronHeart = 4,
                Veinrender = 4,
                UmbilicusEternus = 1,
                MouthOfHell = 1,
                DanceOfDarkness = 4,
                MeatShield = 4,
                Coagulopathy = 4,
                FortitudeOfTheEbonBlade = 1,
                CarrionFeast = 4,
                VampiricAura = 1,
                Souldrinker = 1,
                Concordance = 6
            };

            foreach (var buff in MawOfTheDamned.GetArtifactBuffs())
                Buffs.AddBuff(buff);
            /*
            Buffs.AddBuff(new Buffs.DeathKnight.SetBonuses.T19_2Pc());
            Buffs.AddBuff(new Buffs.DeathKnight.SetBonuses.T19_4Pc(rng));
            Buffs.AddBuff(new Buffs.Trinkets.MementoOfAngerboda(rng, 4978));
            Buffs.AddBuff(new Buffs.Trinkets.ChronoShard(rng, 9388));*/

            Reset();
        }

        public Artifacts.DeathKnight MawOfTheDamned { get; set; }

        public override void Reset()
        {
            Buffs.ClearAllNonPermanent();
            Cooldowns.Reset();
            Cooldowns.GCDLength = GCDLength;

            RunicPower = 0;
            RunesAvailable = 6;

            RuneCounters = new List<decimal>();
            
            CurrentHealth = MaxHealth;
        }

        [XmlIgnore]
        public override decimal ParryChance
        {
            get
            {
                return BaseParry
                    + GetDiminishedAvoidance(
                        RatingConverter.GetRating(StatType.Parry, (int)(CritRating + Buffs.GetRatingAdjustment(StatType.Parry)))
                        +RatingConverter.GetRating(StatType.PrimaryAvoidance, Strength)
                        + Buffs.GetPercentageAdjustment(StatType.Parry));
            }
        }

        private int _runicPower;

        public int RunicPower
        {
            get { return _runicPower; }
            set { _runicPower = Math.Min(value, RunicPowerCap); }
        }

        private int _runicPowerCap;

        public int RunicPowerCap
        {
            get { return _runicPowerCap + Buffs.GetRatingAdjustment(StatType.ResourceCap); }
            set { _runicPowerCap = value; }
        }

        public int RunesAvailable { get; set; }

        #region class-specific counters

        private List<decimal> RuneCounters;

        #endregion

        #region class-specific calculated values

        [XmlIgnore]

        public override int AttackPower
        {
            get
            {
                return (int)(
                    (Strength
                        + Buffs.GetRatingAdjustment(StatType.AttackPower))
                    * (1 + Buffs.GetPercentageAdjustment(StatType.AttackPower)));
            }
        }

        #endregion 


        public override Abilities.Ability GetAbilityUsed(IBuffManager MobBuffs)
        {
            if (Cooldowns.OffGCD)
            {
                var disease = MobBuffs.GetBuff(typeof(BloodPlague));

                if ((disease == null || disease.TimeRemaining <= GCDLength * 2) && Cooldowns.AbilityReady<Abilities.DeathKnight.BloodBoil>())
                {
                    return AbilityManger.GetAbility<Abilities.DeathKnight.BloodBoil>();
                }

                if (Buffs.GetBuff(typeof(CrimsonScourge)) != null)
                {
                    return AbilityManger.GetAbility<Abilities.DeathKnight.DeathAndDecay>();
                }

                var boneShield = Buffs.GetBuff(typeof(BoneShield));

                if (CurrentHealth <= MaxHealth * 0.25 && RunicPower >= 45 &&
                    (boneShield != null && boneShield.Stacks > 2 && boneShield.TimeRemaining >= 5.0m))
                    return AbilityManger.GetAbility<Abilities.DeathKnight.DeathStrike>();

                if (RunesAvailable >= 2 && (boneShield == null || boneShield.Stacks <= 6 || boneShield.TimeRemaining <= 5.0m))
                    return AbilityManger.GetAbility<Abilities.DeathKnight.Marrowrend>();

                if(Buffs.GetBuff<Tank.Buffs.DeathKnight.SetBonuses.T20_2Pc>()!=null)
                {
                    var graveWarden = Buffs.GetBuff<Buffs.DeathKnight.SetBonuses.GraveWarden>();
                    if (graveWarden == null || graveWarden.Durration <= GCDLength * 2)
                        return AbilityManger.GetAbility<Abilities.DeathKnight.BloodBoil>();
                }

                if (CurrentHealth <= MaxHealth * 0.5 && RunicPower >= 45)
                    return AbilityManger.GetAbility<Abilities.DeathKnight.DeathStrike>();

                if (CurrentHealth < MaxHealth * 0.9 && RunicPower >= 80)
                    return AbilityManger.GetAbility<Abilities.DeathKnight.DeathStrike>();

                if (RunicPower >= 45 && Abilities.DeathKnight.DeathStrike.HealingAmount(this) >= MaxHealth * 0.40m)
                    return AbilityManger.GetAbility<Abilities.DeathKnight.DeathStrike>();

                if (RunicPower >= 80)
                    return AbilityManger.GetAbility<Abilities.DeathKnight.DeathStrike>();

                if (MawOfTheDamned.Consumption > 0 && Cooldowns.AbilityReady<Abilities.DeathKnight.Consumption>())
                {
                    return AbilityManger.GetAbility<Abilities.DeathKnight.Consumption>();
                }

                if (Cooldowns.AbilityReady<Abilities.DeathKnight.BloodBoil>())
                {
                    return AbilityManger.GetAbility<Abilities.DeathKnight.BloodBoil>();
                }
                
                if (RunesAvailable > 0 && boneShield!=null && boneShield.Stacks>=5)
                {
                    if (Cooldowns.AbilityReady<Abilities.DeathKnight.DeathAndDecay>())
                        return AbilityManger.GetAbility<Abilities.DeathKnight.DeathAndDecay>();
                    else
                        return AbilityManger.GetAbility<Abilities.DeathKnight.HeartStrike>();
                }
            }

            if (HealthPercentage <= 0.75m && Cooldowns.AbilityReady<Abilities.DeathKnight.VampiricBlood>())
                return AbilityManger.GetAbility<Abilities.DeathKnight.VampiricBlood>();

            return null;
        }

        public override void UpdateAbilityResults(decimal CurrentTime, Abilities.Ability Ability, AbilityResult Result)
        {
            if (Buffs.GetBuff(typeof(DeathAndDecay)) != null && Result.ResourceCost < 0)
                Result.ResourceCost = (int)(Result.ResourceCost * 1.15m);
            RunicPower -= Result.ResourceCost;
            if (RunicPower > RunicPowerCap)
                RunicPower = RunicPowerCap;
            if (Result.SecondaryResourceCost > 0)
            {
                RunesAvailable -= Result.SecondaryResourceCost;
                for (var i = 0; i < Result.SecondaryResourceCost; i++)
                    if (RuneCounters.Count < 3)
                        RuneCounters.Add(10.0m / (1.0m + Haste));
            }

            //ApplyHealing(Result.SelfHealing);
        }

        public override void UpdateTimeElapsed(decimal DeltaTime)
        {
            base.UpdateTimeElapsed(DeltaTime);

            for (var i = 0; i < RuneCounters.Count; i++)
                RuneCounters[i] -= DeltaTime;

            RunesAvailable += RuneCounters.Count(rc => rc <= 0);
            RuneCounters = RuneCounters.Where(rc => rc > 0).ToList();
            while (RuneCounters.Count < 3 && RuneCounters.Count + RunesAvailable < 6)
                RuneCounters.Add(10.0m / (1.0m + Haste));
        }
        
        public override DataLogging.DamageEvent UpdateFromMobAttack(DataLogging.DamageEvent DamageEvent)
        {
            return DamageEvent;
        }
    }
}
