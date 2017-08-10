namespace Tank.Buffs.Druid
{
    public class SurvivalInstincts : Buff
    {
        public override decimal Durration { get { return 6m; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.DamageReduction)
                return 0.50m;
            return 0;
        }
    }
}