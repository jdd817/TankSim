using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Abilities
{
    public class AbilityResult
    {
        public AbilityResult()
        {
            CasterBuffsApplied = new List<Buffs.Buff>();
            TargetBuffsApplied = new List<Buffs.Buff>();
            CooldownReduction = new List<Abilities.CooldownReduction>();
            ChannelLength = 0;
        }
        public decimal Time { get; set; }
        public AttackResult AttackResult { get; set; }
        public int ResourceCost { get; set; }
        public int SecondaryResourceCost { get; set; }
        public List<Buffs.Buff> CasterBuffsApplied { get; set; }
        public List<Buffs.Buff> TargetBuffsApplied { get; set; }
        public int DamageDealt { get; set; }
        public int SelfHealing { get; set; }
        public int TartgetHealing { get; set; }
        public List<CooldownReduction> CooldownReduction { get; set; }
        public decimal ChannelLength { get; set; }
    }

    public class CooldownReduction
    {
        public Type Ability { get; set; }
        public ReductionType ReductionType { get; set; }
        public decimal Amount { get; set; }
    }

    public enum ReductionType
    {
        To,
        By
    }
}
