﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

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
            //Result.SelfHealing = ApplyHealing(Result.SelfHealing);

            if (Ability.GetType()==typeof(Abilities.Monk.PurifyingBrew))
            {
                var stagger = Buffs.GetBuff<Buffs.Monk.Stagger>();
                if(stagger!=null)
                {
                    stagger.DamageDelayed = stagger.DamageDelayed / 2;
                }
            }
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
                if (1.0m * CurrentHealth / MaxHealth <= 0.50m)
                    absorbOrbs = true;
                if (healingOrbs.TimeRemaining <= 0.25m)
                    absorbOrbs = true;

                if(absorbOrbs)
                {
                    var healingAmount = ApplyHealing((int)(healingOrbs.Stacks * AttackPower * 7.5m));
                    Buffs.ClearBuff<Buffs.Monk.HealingOrb>();
                    DataLogging.DataLogManager.UsedAbility(DataLogging.DataLogManager.CurrentTime, "Healed", new AbilityResult
                    {
                        SelfHealing = healingAmount
                    });
                }
            }
        }

        public override DataLogging.DamageEvent UpdateFromMobAttack(DataLogging.DamageEvent DamageEvent)
        {
            if (DamageEvent.Result == AttackResult.Dodge && Buffs.GetBuff(typeof(Buffs.Monk.ElusiveBrawler)) != null)
                Buffs.ClearBuff(typeof(Buffs.Monk.ElusiveBrawler));
            
            if (DamageEvent.DamageTaken > 0)
            {
                Buffs.AddBuff(new Buffs.Monk.ElusiveBrawler(Mastery));

                var staggerAmount = 0.45m;
                if (Buffs.GetBuff(typeof(Buffs.Monk.IronskinBrew)) != null)
                    staggerAmount += 0.40m;

                var damageStaggered = (int)(DamageEvent.DamageTaken * staggerAmount);
                var healingOrbChance = (0.75m * DamageEvent.DamageTaken / MaxHealth) * (3 - 2m * CurrentHealth / MaxHealth);

                DamageEvent.DamageTaken = DamageEvent.DamageTaken - damageStaggered;
                
                if (Rng.NextDouble() <= (double)healingOrbChance)
                    Buffs.AddBuff(new Buffs.Monk.HealingOrb());

                Buffs.AddBuff(new Buffs.Monk.Stagger(damageStaggered));

                if (CurrentHealth + DamageEvent.DamageTaken > MaxHealth * 0.35m && CurrentHealth < MaxHealth * 0.35m && Cooldowns.ChargesAvailable<Abilities.Monk.HealingElixer>() > 0)
                {
                    var elixer = new Abilities.Monk.HealingElixer();
                    var result = elixer.GetAbilityResult(AttackResult.Hit, this, null);
                    result.SelfHealing = ApplyHealing(result.SelfHealing);
                    Cooldowns.AbilityUsed(elixer, result);
                    DataLogging.DataLogManager.UsedAbility(DamageEvent.Time, elixer.GetType().Name, result);
                }
            }

            return DamageEvent;
        }

        public override void UpdateFromTickingBuffs(IEnumerable<Buffs.Buff> TickingBuffs)
        {
            base.UpdateFromTickingBuffs(TickingBuffs);
            foreach (var stagger in TickingBuffs.OfType<Buffs.Monk.Stagger>())
            {
                int damageTaken;
                if (stagger.TimeRemaining > 0)
                    damageTaken = (int)(stagger.DamageDelayed / stagger.TimeRemaining * stagger.Tick);
                else
                    damageTaken = stagger.DamageDelayed;
                CurrentHealth -= damageTaken;
                stagger.DamageDelayed -= damageTaken;
                DataLogging.DataLogManager.LogEvent(new DataLogging.DamageEvent
                {
                    Name = "Stagger",
                    Time = DataLogging.DataLogManager.CurrentTime,
                    DamageTaken = damageTaken,
                    Result = AttackResult.Hit
                });
            }
        }

        public override int ApplyHealing(int healingAmount)
        {
            var actualHealing = healingAmount;
            if (Rng.NextDouble() < (double)CritChance)
                actualHealing= (int)( healingAmount * 1.65m);
            
            CurrentHealth += actualHealing;

            return actualHealing;
        }
    }
}
