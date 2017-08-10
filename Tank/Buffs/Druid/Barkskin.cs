namespace Tank.Buffs.Druid
{
    public class Barkskin : Buff
    {
        public override decimal Durration { get { return 12m; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.DamageReduction)
                return 0.20m;
            return 0;
        }
    }
}