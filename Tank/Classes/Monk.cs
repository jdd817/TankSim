﻿using System;
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

            Buffs.AddBuff(new Buffs.Monk.GiftOfTheOxAura());
            Buffs.AddBuff(new Buffs.Monk.StaggerAura());
            Buffs.AddBuff(new Buffs.Monk.CelestialFortuneAura(rng));
                        
            FuZan = new Artifacts.Monk(rng)
            {
                ExplodingKeg = 1,
                FullKeg = 1,
                PotentKick = 4,
                HotBlooded = 4,
                Smashed = 1,
                Overflow = 4,
                BrewStache = 1,
                FacePalm = 4,
                ObstinateDetermination = 1,
                ObsidianFists = 4,
                DragonfireBrew = 1,
                HealthyAppetite = 4,
                GiftedStudent = 4,
                StaggeringAround = 4,
                Fortification = 1,
                SwiftAsACoursingRiver = 1,
                DarkSideOfTheMoon = 4,
                EnduranceOfTheBrokenTemple = 1,
                DraughtOfDarkness = 4,
                StaveOff = 1,
                QuickSip = 1,
                Concordance = 6
            };

            foreach (var buff in FuZan.GetArtifactBuffs())
                Buffs.AddBuff(buff);

            Reset();
        }

        public Artifacts.Monk FuZan { get; set;}

        public override void Reset()
        {
            Buffs.ClearAllNonPermanent();
            Cooldowns.Reset();
            Cooldowns.GCDLength = GCDLength;

            Energy = 0;
            CurrentHealth = MaxHealth;

            rotation = 0;
            Buffs.GetBuff<Buffs.Monk.GiftOfTheOxAura>().DamageCounter = 0;
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

        private int rotation;


        public override Abilities.Ability GetAbilityUsed(IBuffManager MobBuffs)
        {
            if (Buffs.GetBuff<Talents.Monk.BlackoutCombo>() == null)
            {
                if (Cooldowns.OffGCD)
                {
                    if (Cooldowns.AbilityReady<Abilities.Monk.ExplodingKeg>())
                        return AbilityManger.GetAbility<Abilities.Monk.ExplodingKeg>();
                    if (Cooldowns.AbilityReady<Abilities.Monk.KegSmash>() && Energy >= 40)
                        return AbilityManger.GetAbility<Abilities.Monk.KegSmash>();
                    if (Cooldowns.AbilityReady<Abilities.Monk.BlackoutStrike>())
                        return AbilityManger.GetAbility<Abilities.Monk.BlackoutStrike>();
                    if (Cooldowns.AbilityReady<Abilities.Monk.BreathOfFire>())
                        return AbilityManger.GetAbility<Abilities.Monk.BreathOfFire>();
                    if (Energy >= 50)
                        return AbilityManger.GetAbility<Abilities.Monk.TigerPalm>();
                }

                var brew = GetBrewUsage();

                if (brew != null)
                    return brew;
            }
            else
            {
                if (Cooldowns.OffGCD)
                {
                    if (rotation == 0 && Cooldowns.AbilityReady<Abilities.Monk.KegSmash>() && Energy >= 40)
                    {
                        rotation++;
                        return AbilityManger.GetAbility<Abilities.Monk.KegSmash>();
                    }
                    if ((rotation == 1 || rotation == 4) && Cooldowns.AbilityReady<Abilities.Monk.BlackoutStrike>())
                    {
                        rotation++;
                        return AbilityManger.GetAbility<Abilities.Monk.BlackoutStrike>();
                    }
                    if ((rotation == 2 || rotation == 5) && Cooldowns.AbilityReady<Abilities.Monk.TigerPalm>() && Energy >= 50)
                    {
                        rotation++;
                        return AbilityManger.GetAbility<Abilities.Monk.TigerPalm>();
                    }

                    if(rotation==3 || rotation==6)
                    {
                        rotation++;
                        if (Cooldowns.AbilityReady<Abilities.Monk.ExplodingKeg>())
                            return AbilityManger.GetAbility<Abilities.Monk.ExplodingKeg>();
                        if (Cooldowns.AbilityReady<Abilities.Monk.BreathOfFire>())
                            return AbilityManger.GetAbility<Abilities.Monk.BreathOfFire>();
                        //if(Cooldowns.AbilityReady<Abilities.Monk.RushingJadeWind>)
                    }
                }

                if(!Buffs.BuffActive<Talents.Monk.BlackoutCombo_Buff>())
                {
                    var brew = GetBrewUsage();

                    if (brew != null)
                        return brew;
                }
            }

            if (HealthPercentage <= 0.85m
                && Buffs.BuffActive<Talents.Monk.HealingElixer>()
                && Cooldowns.ChargesAvailable<Abilities.Monk.HealingElixer>() == 2)
                return AbilityManger.GetAbility<Abilities.Monk.HealingElixer>();

            if (HealthPercentage <= 0.60m
                && Cooldowns.AbilityReady<Abilities.Monk.FortifyingBrew>())
                return AbilityManger.GetAbility<Abilities.Monk.FortifyingBrew>();

            if (HealthPercentage <= 0.60m
                && Buffs.BuffActive<Talents.Monk.DampenHarm>()
                && Cooldowns.AbilityReady<Abilities.Monk.DampenHarm>()
                && !Buffs.BuffActive<Buffs.Monk.FortifyingBrew>())
                return AbilityManger.GetAbility<Abilities.Monk.DampenHarm>();

            return null;
        }

        private Abilities.Ability GetBrewUsage()
        {
            if (HealthPercentage <= 0.5m
                        && Cooldowns.ChargesAvailable<Abilities.Monk.IronskinBrew>() >= 2
                        && Buffs.GetBuff(typeof(Buffs.Monk.IronskinBrew)) == null)
                return AbilityManger.GetAbility<Abilities.Monk.IronskinBrew>();


            //both ironskin and purifying brews are tracked under ironskin
            var stagger = Buffs.GetBuff(typeof(Buffs.Monk.Stagger)) as Buffs.Monk.Stagger;
            if (stagger != null && stagger.DamageDelayed >= MaxHealth * 0.10m && Cooldowns.AbilityReady<Abilities.Monk.IronskinBrew>() && !Buffs.BuffActive<Buffs.Monk.Artifact.BrewStache_Buff>())
                return AbilityManger.GetAbility<Abilities.Monk.PurifyingBrew>();

            if (stagger != null && Cooldowns.ChargesAvailable<Abilities.Monk.IronskinBrew>() == 3 && stagger.DamageDelayed >= MaxHealth * 0.05m)
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
