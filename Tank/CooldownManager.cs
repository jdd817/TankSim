using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank
{
    public class CooldownManager
    {
        public CooldownManager()
        {
            Cooldowns = new Dictionary<Type, CooldownInfo>(); 
        }
        
        private Dictionary<Type, CooldownInfo> Cooldowns;

        public bool OffGCD
        {
            get { return GCDTimer <= 0; }
        }

        public decimal GCDLength { get; set; }
        public decimal GCDTimer { get; private set; }

        public bool AbilityReady<T>() where T : Ability
        {
            return AbilityReady(typeof(T));
        }

        public bool AbilityReady(Type Ability)
        {
            if (!Cooldowns.ContainsKey(Ability))
                return true;
            return Cooldowns[Ability].Charges > 0;
        }

        public int ChargesAvailable<T>() where T :Ability
        {
            return ChargesAvailable(typeof(T));
        }

        public int ChargesAvailable(Type Ability)
        {
            if (!Cooldowns.ContainsKey(Ability))
            {
                var ability = (Ability)Activator.CreateInstance(Ability);
                Cooldowns.Add(ability.GetType(), new CooldownInfo { Charges = ability.MaxCharges, MaxCharges = ability.MaxCharges, CooldownLength = ability.Cooldown });
            }
            return Cooldowns[Ability].Charges;
        }

        public void AbilityUsed(Ability ability, AbilityResult result)
        {
            if (ability.OnGCD)
                GCDTimer = GCDLength;
            if (!Cooldowns.ContainsKey(ability.CooldownType))
                Cooldowns.Add(ability.CooldownType, new CooldownInfo { Charges = ability.MaxCharges, MaxCharges = ability.MaxCharges, CooldownLength = ability.Cooldown });

            var cdInfo = Cooldowns[ability.CooldownType];
            if (cdInfo.Charges == cdInfo.MaxCharges)
                cdInfo.CooldownRemaining = cdInfo.CooldownLength;
            cdInfo.Charges--;

            ReduceTimers(result.CooldownReduction);
        }

        public void UpdateTimers(decimal DeltaTime)
        {
            foreach (var ability in Cooldowns.Values)
            {
                if (ability.Charges < ability.MaxCharges)
                {
                    ability.CooldownRemaining -= DeltaTime;
                    if (ability.CooldownRemaining <= 0)
                    {
                        ability.Charges++;
                        if (ability.Charges < ability.MaxCharges)
                            ability.CooldownRemaining += ability.CooldownLength;
                    }
                }
            }
            if (GCDTimer > 0)
                GCDTimer -= DeltaTime;
        }

        public void ReduceTimers(params CooldownReduction[] reductions)
        {
            foreach (var cdReduction in reductions)
            {
                if (!Cooldowns.ContainsKey(cdReduction.Ability))
                    continue;
                var cdInfo = Cooldowns[cdReduction.Ability];

                if (cdInfo.CooldownRemaining <= 0 && cdInfo.Charges == cdInfo.MaxCharges)
                    continue;

                if (cdReduction.ReductionType == ReductionType.To)
                    cdInfo.CooldownRemaining = cdReduction.Amount;
                else
                    cdInfo.CooldownRemaining -= cdReduction.Amount;

                if (cdInfo.CooldownRemaining <= 0)
                {
                    cdInfo.Charges++;
                    if (cdInfo.Charges < cdInfo.MaxCharges)
                        cdInfo.CooldownRemaining += cdInfo.CooldownLength;
                }
            }
        }

        public void Reset()
        {
            Cooldowns = new Dictionary<Type, CooldownInfo>();
        }

        class CooldownInfo
        {
            public int Charges { get; set; }
            public decimal CooldownRemaining { get; set; }
            public int MaxCharges { get; set; }
            public decimal CooldownLength { get; set; }
        }
    }    
}
