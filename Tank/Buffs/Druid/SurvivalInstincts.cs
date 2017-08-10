namespace Tank.Buffs.Druid
{
    public class SurvivalInstincts : Buff
    {
        public SurvivalInstincts()
        {
            DamageReduction = 0.50m;
        }

        public override decimal Durration { get { return 6m; } }

        public decimal DamageReduction { get; set; }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.DamageReduction)
                return DamageReduction;
            return 0;
        }
    }
}