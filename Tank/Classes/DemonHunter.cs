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

            Pain = 0;
            CurrentHealth = MaxHealth;

            ImmolationAuraCD = 0;
            SigilOfFlameCD = 0;
            DemonSpikesRecharge = 0;
            DemonSpikesCharges = 2;
            FelBladeCD = 0;
            FelDevestationCD = 0;
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

        private decimal ImmolationAuraCD;
        private decimal SigilOfFlameCD;
        private decimal DemonSpikesRecharge;
        private int DemonSpikesCharges;
        private decimal FelBladeCD;
        private decimal FelDevestationCD;

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
            if (GCD <= 0)
            {
                if (Pain >= 30 && FelDevestationCD <= 0 && HealthPercentage <= 0.90m)
                {
                    FelDevestationCD = 60m;
                    return new Abilities.DemonHunter.FelDevestation();
                }
                if (Pain >= 80)
                    return new Abilities.DemonHunter.SoulCleave();
                if (ImmolationAuraCD <= 0)
                {
                    ImmolationAuraCD = 15.0m;
                    return new Abilities.DemonHunter.ImmolationAura();
                }
                if (SigilOfFlameCD <= 0)
                {
                    SigilOfFlameCD = 30.0m;
                    return new Abilities.DemonHunter.SigilOfFlame();
                }
                if(FelBladeCD<=0)
                {
                    FelBladeCD = 15m;
                    return new Abilities.DemonHunter.FelBlade();
                }
                return new Abilities.DemonHunter.Shear();
            }

            if (DemonSpikesCharges > 0 && Pain >= 20 && Buffs.GetBuff(typeof(Buffs.DemonHunter.DemonSpikes)) == null)
            {
                DemonSpikesCharges--;
                if (DemonSpikesCharges == 1)
                    DemonSpikesRecharge = 15;
                return new Abilities.DemonHunter.DemonSpikes();
            }

            return null;
        }

        public override void UpdateAbilityResults(decimal CurrentTime, Abilities.Ability Ability, AbilityResult Result)
        {
            Pain -= Result.ResourceCost;
            CurrentHealth += Result.SelfHealing;
            foreach(var cdReduction in Result.CooldownReduction)
            {
                if(cdReduction.Ability==typeof(Abilities.DemonHunter.FelBlade))
                {
                    if (cdReduction.ReductionType == ReductionType.To)
                        FelBladeCD = cdReduction.Amount;
                    else
                        FelBladeCD = Math.Max(0, FelBladeCD - cdReduction.Amount);
                }
                if (cdReduction.Ability == typeof(Abilities.DemonHunter.DemonSpikes))
                {
                    if (cdReduction.ReductionType == ReductionType.To)
                        DemonSpikesRecharge = cdReduction.Amount;
                    else
                        DemonSpikesRecharge = Math.Max(0, FelBladeCD - cdReduction.Amount);
                    if (DemonSpikesRecharge <= 0 && DemonSpikesCharges < 2)
                    {
                        DemonSpikesCharges++;
                        if (DemonSpikesCharges < 2)
                            DemonSpikesRecharge += 15;
                    }
                }
            }
        }

        public override void UpdateTimeElapsed(decimal DeltaTime)
        {
            UpdateTimers(DeltaTime);

            if (DemonSpikesCharges < 2)
            {
                DemonSpikesRecharge -= DeltaTime;
                if (DemonSpikesRecharge <= 0)
                {
                    DemonSpikesCharges++;
                    if (DemonSpikesCharges < 2)
                        DemonSpikesRecharge += 15;
                }
            }

            ImmolationAuraCD = Math.Max(0, ImmolationAuraCD - DeltaTime);
            SigilOfFlameCD = Math.Max(0, SigilOfFlameCD - DeltaTime);
            FelBladeCD = Math.Max(0, FelBladeCD - DeltaTime);
            FelDevestationCD = Math.Max(0, FelDevestationCD - DeltaTime);
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
