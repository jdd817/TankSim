using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Tank.Abilities;
using Tank.Buffs;

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
        static DemonHunter()
        {
            V = 0.0315m;
            BaseDodge = 0.03m;
            BaseParry = 0.10m;
        }

        public DemonHunter()
        {
            Reset();
        }

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

        private int Pain
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
                    * (1 + Buffs.GetPercentageAdjustment(StatType.AttackPower)));
            }
        }

        #endregion 


        public override Abilities.Ability GetAbilityUsed(BuffManager MobBuffs)
        {
            if (Cooldowns.OffGCD)
            {
                if (Pain >= 30 && Cooldowns.AbilityReady<Abilities.DemonHunter.FelDevestation>() && HealthPercentage <= 0.90m)
                    return new Abilities.DemonHunter.FelDevestation();

                if (Pain >= 80)
                    return new Abilities.DemonHunter.SoulCleave();

                if (Cooldowns.AbilityReady<Abilities.DemonHunter.ImmolationAura>())
                    return new Abilities.DemonHunter.ImmolationAura();

                if (Cooldowns.AbilityReady<Abilities.DemonHunter.SigilOfFlame>())
                    return new Abilities.DemonHunter.SigilOfFlame();

                if(Cooldowns.AbilityReady<Abilities.DemonHunter.FelBlade>())
                    return new Abilities.DemonHunter.FelBlade();

                return new Abilities.DemonHunter.Shear();
            }

            if (Cooldowns.AbilityReady<Abilities.DemonHunter.DemonSpikes>() && Pain >= 20 && Buffs.GetBuff(typeof(Buffs.DemonHunter.DemonSpikes)) == null)
                return new Abilities.DemonHunter.DemonSpikes();

            return null;
        }

        public override void UpdateAbilityResults(decimal CurrentTime, Abilities.Ability Ability, AbilityResult Result)
        {
            Pain -= Result.ResourceCost;
            ApplyHealing(Result.SelfHealing);
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
                DamageEvent.DamageTaken = 0;
            
            //get pain
            //absent finding a blue post regarding this, using the same formula as warrior rage
            Pain += (int)((50m * DamageEvent.DamageTaken) / MaxHealth);

            DamageEvent.DamageTaken = (int)(DamageEvent.DamageTaken * (1m - VersatilityDamageReduction) * (1m - Buffs.GetPercentageAdjustment(StatType.DamageReduction)));

            CurrentHealth -= DamageEvent.DamageTaken;

            DataLogging.DataLogManager.LogEvent(DamageEvent);
        }

        public override void UpdateFromTickingBuffs(IEnumerable<Buffs.Buff> TickingBuffs)
        {
            base.UpdateFromTickingBuffs(TickingBuffs);
            foreach (var aura in TickingBuffs.OfType<Buffs.DemonHunter.ImmolationAura>())
                Pain += 2;
        }
    }
}
