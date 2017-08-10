namespace Tank.Buffs.Druid
{
    public class Pulverize : Buff
    {
        public override decimal Durration { get { return 20m; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.DamageReduction)
                return 0.09m;

            return 0;
        }
    }
}