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
            BaseParry = 0.10m;
            BaseBlock = 0.00m;

            RunicPowerCap = 125;

            UsesTwoHanders = true;

            Reset();
        }

        public override void Reset()
        {
            Buffs.ClearAllNonPermanent();
            Cooldowns.Reset();
            Cooldowns.GCDLength = GCDLength;

            RunicPower = 0;
            RunesAvailable = 6;

            RuneCounters = new List<decimal>();
            HitsLast5Secs = new List<decimal>();
            
            CurrentHealth = MaxHealth;

            //blues state this 0.018, testing indicates the number below - changed since said blue post? or armory not correct?
            //V = 0.0235m;
            V = 0.039m;
    }

    [XmlIgnore]
        public override decimal ParryChance
        {
            get
            {
                return BaseParry
                    + GetDiminishedAvoidance(
                        RatingConverter.GetRating(StatType.Parry, (int)(CritRating + Buffs.GetRatingAdjustment(StatType.Parry)))
                        + Buffs.GetPercentageAdjustment(StatType.Parry));
            }
        }

        private int _runicPower;

        private int RunicPower
        {
            get { return _runicPower; }
            set { _runicPower = Math.Min(value, RunicPowerCap); }
        }

        public int RunicPowerCap
        { get; set; }

        public int RunesAvailable { get; set; }

        #region class-specific counters

        private List<decimal> RuneCounters;
        private List<decimal> HitsLast5Secs;

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
                    (boneShield != null && boneShield.TimeRemaining >= 5.0m))
                    return AbilityManger.GetAbility<Abilities.DeathKnight.DeathStrike>();

                if (HitsLast5Secs.Count <= 5 && RunesAvailable >= 2 && (boneShield == null || boneShield.Stacks <= 6 || boneShield.TimeRemaining <= 5.0m))
                    return AbilityManger.GetAbility<Abilities.DeathKnight.Marrowrend>();

                if (CurrentHealth <= MaxHealth * 0.5 && RunicPower >= 45)
                    return AbilityManger.GetAbility<Abilities.DeathKnight.DeathStrike>();

                if (RunicPower >= 80)
                    return AbilityManger.GetAbility<Abilities.DeathKnight.DeathStrike>();

                if (RunicPower >= 45 && Abilities.DeathKnight.DeathStrike.HealingAmount(this) >= MaxHealth * 0.40m)
                    return AbilityManger.GetAbility<Abilities.DeathKnight.DeathStrike>();

                if (Buffs.GetStacks(typeof(Artifact.Consumption)) > 0 && Cooldowns.AbilityReady<Abilities.DeathKnight.Consumption>() && CurrentHealth <= MaxHealth * 0.80m)
                {
                    return AbilityManger.GetAbility<Abilities.DeathKnight.Consumption>();
                }

                if (Cooldowns.AbilityReady<Abilities.DeathKnight.BloodBoil>())
                {
                    return AbilityManger.GetAbility<Abilities.DeathKnight.BloodBoil>();
                }

                if (HitsLast5Secs.Count > 5)
                {
                    if (RunesAvailable > 0)
                    {
                        if (Cooldowns.AbilityReady<Abilities.DeathKnight.DeathAndDecay>())
                            return AbilityManger.GetAbility<Abilities.DeathKnight.DeathAndDecay>();
                        else
                            return AbilityManger.GetAbility<Abilities.DeathKnight.HeartStrike>();
                    }
                }
                else if (RunesAvailable >= 4 || (RunesAvailable >= 1
                        && (boneShield != null && boneShield.Stacks >= 5
                            && (RunesAvailable >= 3 || (RuneCounters.Where(rc => rc < boneShield.TimeRemaining - GCDLength).Count() + RunesAvailable) >= 3))))
                {
                    if (Cooldowns.AbilityReady<Abilities.DeathKnight.DeathAndDecay>())
                        return AbilityManger.GetAbility<Abilities.DeathKnight.DeathAndDecay>();
                    else
                        return AbilityManger.GetAbility<Abilities.DeathKnight.HeartStrike>();
                }
            }

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

            ApplyHealing(Result.SelfHealing);
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

        private decimal _lastMobHit = -10;
        private bool _ignoreBoneShieldStacks = false;

        public override DataLogging.DamageEvent UpdateFromMobAttack(DataLogging.DamageEvent DamageEvent)
        {
            HitsLast5Secs.Add(DamageEvent.Time);
            HitsLast5Secs = HitsLast5Secs.Where(t => t >= DamageEvent.Time - 5).ToList();

            _lastMobHit = DamageEvent.Time;
            
            if (DamageEvent.DamageTaken > 0)
            {
                var boneShield = (BoneShield)Buffs.GetBuff(typeof(BoneShield));
                if (boneShield != null && boneShield.Stacks > 0)
                {
                    boneShield.Stacks--;
                }
            }

            //need to move absorbs to the combat engine as well
            BloodShield Shield = (BloodShield)Buffs.GetBuff(typeof(BloodShield));
            if (Shield != null)
            {
                int Absorbed = Math.Min(Shield.DamageRemaining, DamageEvent.DamageTaken);
                DamageEvent.DamageTaken = DamageEvent.DamageTaken - Absorbed;
                DamageEvent.DamageAbsorbed = Absorbed;
                Shield.DamageRemaining -= Absorbed;
            }

            return DamageEvent;
        }
    }
}
