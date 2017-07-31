using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Tank.Buffs.Warrior;
using System.Xml.Serialization;
using Tank.Abilities;
using Tank.DataLogging;

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

            UsesTwoHanders = false;

            Buffs.AddBuff(new Block(rng));

            ScaleOfTheEarthWarder = new Artifacts.Warrior(rng)
            {
                BastionOfTheAspects = 4,
                DragonScales = 1,
                DragonSkin = 4,
                GleamingScales = 1,
                Intolerance = 4,
                LeapingGiants = 4,
                MightOfTheVrykul = 1,
                ProtectionOfTheValarjar = 1,
                RageOfTheFallen = 4,
                ReflectivePlating = 1,
                RumblingVoice = 1,
                ScalesOfEarth = 1,
                ShatterTheBones = 4,
                ThunderCrash = 4,
                Toughness = 4,
                VrykulShieldTraining = 4,
                WallOfSteel = 1,
                WillToSurvive = 4,
                Concordance = 1
            };

            foreach (var buff in ScaleOfTheEarthWarder.GetArtifactBuffs())
                Buffs.AddBuff(buff);

            Reset();
        }

        public Artifacts.Warrior ScaleOfTheEarthWarder { get; set; }

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

        public int Rage
        {
            get { return _rage; }
            set { _rage = Math.Min(value, RageCap); }
        }

        public int _rageCap;

        public int RageCap
        {
            get { return _rageCap + Buffs.GetRatingAdjustment(StatType.ResourceCap); }
            set { _rageCap = value; }
        }

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

        #region class auras

        [Buffs.EffectPriority(-5)]
        private class Block : Buffs.PermanentBuff, Buffs.IDamageTakenEffectStack
        {
            private IRng _rng;

            public Block(IRng rng)
            {
                _rng = rng;
            }

            public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
            {
                if (damageEvent.Result == AttackResult.Block)
                {
                    var damageBlocked = 0;
                    if (_rng.NextDouble() <= (double)(Target as Warrior).CritBlockChance)
                        damageBlocked = (int)(damageEvent.DamageTaken * 0.60);
                    else
                        damageBlocked = (int)(damageEvent.DamageTaken * 0.30);
                    damageEvent.DamageBlocked = damageBlocked;
                    damageEvent.DamageTaken = damageEvent.DamageTaken - damageBlocked;
                }
            }
        }

        #endregion

        public override Abilities.Ability GetAbilityUsed(IBuffManager MobBuffs)
        {
            if (Cooldowns.OffGCD)
            {
                if (Cooldowns.AbilityReady<Abilities.Warrior.ShieldSlam>())
                    return AbilityManger.GetAbility<Abilities.Warrior.ShieldSlam>();
                if (Cooldowns.AbilityReady<Abilities.Warrior.Revenge>() && Buffs.GetBuff<Buffs.Warrior.Revenge>() != null)
                    return AbilityManger.GetAbility<Abilities.Warrior.Revenge>();
                if (Cooldowns.AbilityReady<Abilities.Warrior.ThunderClap>())
                    return AbilityManger.GetAbility<Abilities.Warrior.ThunderClap>();
                if (Cooldowns.AbilityReady<Abilities.Warrior.Revenge>() && Buffs.GetBuff<Talents.Warrior.Vengeance_Revenge>() != null && Rage >= 60)
                    return AbilityManger.GetAbility<Abilities.Warrior.Revenge>();
                //return AbilityManger.GetAbility<Abilities.Warrior.Devastate>();
            }

            if (Cooldowns.AbilityReady<Abilities.Warrior.ShieldBlock>() && Rage >= 15 && Buffs.GetBuff(typeof(Buffs.Warrior.ShieldBlock)) == null)
                return AbilityManger.GetAbility<Abilities.Warrior.ShieldBlock>();

            if ((Rage >= 60 && Buffs.GetBuff(typeof(IgnorePain)) == null && Buffs.GetBuff< Talents.Warrior.Vengeance_Revenge>()==null) ||
                (Rage >= 40 && Buffs.GetBuff<Talents.Warrior.Vengeance_IgnorePain>() != null))
                return AbilityManger.GetAbility<Abilities.Warrior.IgnorePain>();

            if (Rage <= 20 && Buffs.GetBuff<Buffs.Warrior.SetBonuses.T20_2Pc>() != null && Cooldowns.AbilityReady<Abilities.Warrior.BerserkerRage>())
                return AbilityManger.GetAbility<Abilities.Warrior.BerserkerRage>();

            if (Cooldowns.AbilityReady<Abilities.Warrior.DemoralizingShout>())
                return AbilityManger.GetAbility<Abilities.Warrior.DemoralizingShout>();

            return null;
        }

        public override void UpdateAbilityResults(decimal CurrentTime, Abilities.Ability Ability, AbilityResult Result)
        {
            Rage -= Result.ResourceCost;
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
                Cooldowns.ReduceTimers(new CooldownReduction { Ability = typeof(Abilities.Warrior.Revenge), Amount = 0, ReductionType = ReductionType.By });
                Buffs.AddBuff(new Buffs.Warrior.Revenge());
            }

            //get rage
            //from blue: 50 * DamageTaken / MaxHealth
            Rage += (int)((50m * DamageEvent.RawDamage) / MaxHealth);
            
            return DamageEvent;
        }
    }
}
