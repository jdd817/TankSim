﻿using System;
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

            RageCap = 100;
            Reset();
        }

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

        private int Rage
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
        { get { return 0.15m + RatingConverter.GetRating(StatType.Crit, CritRating); } }

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

        #endregion 


        public override Abilities.Ability GetAbilityUsed(IBuffManager MobBuffs)
        {
            if (Cooldowns.OffGCD)
            {
                if (Buffs.GetBuff<Buffs.Druid.GalacticGuardian>() != null)
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
                    return new Abilities.Druid.Maul();
                return AbilityManger.GetAbility<Abilities.Druid.Swipe>();

            }

            if (Cooldowns.AbilityReady<Abilities.Druid.BristlingFur>())
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
            Result.SelfHealing = ApplyHealing(Result.SelfHealing);
            Rage -= Result.ResourceCost;
            if (Ability.GetType() == typeof(Abilities.Attack))
                Rage += 8;
        }

        public override void UpdateTimeElapsed(decimal DeltaTime)
        {
            base.UpdateTimeElapsed(DeltaTime);

            if(YserasGiftCD<=0 && CurrentHealth<MaxHealth)
            {
                YserasGiftCD = 5m;
                var healingAmount = (int)(MaxHealth * 0.03m);
                CurrentHealth += healingAmount;
                DataLogging.DataLogManager.UsedAbility(DataLogging.DataLogManager.CurrentTime,"Healed", new AbilityResult
                {
                    SelfHealing = healingAmount
                });
            }
            
            YserasGiftCD = Math.Max(0, YserasGiftCD - DeltaTime);
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
                DamageEvent.DamageTaken = 0;
            }

            DamageEvent.DamageTaken = (int)(DamageEvent.DamageTaken * (1m - VersatilityDamageReduction));

            //get rage
            //from blue: 50 * DamageTaken / MaxHealth
            if (Buffs.GetBuff<Buffs.Druid.BristlingFur>() != null)
                Rage += (int)((100m * DamageEvent.DamageTaken) / MaxHealth);

            if(Rng.NextDouble()<=0.10)
            {
                Buffs.AddBuff(new Buffs.Druid.GalacticGuardian());
            }

            CurrentHealth -= DamageEvent.DamageTaken;

            DataLogging.DataLogManager.LogEvent(DamageEvent);
        }

        public override int ApplyHealing(int healingAmount)
        {
            return base.ApplyHealing((int)(healingAmount * (1 + Mastery * 1.25m)));
        }
    }
}
