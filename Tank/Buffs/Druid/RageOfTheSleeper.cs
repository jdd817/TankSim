namespace Tank.Buffs.Druid
{
    public class RageOfTheSleeper : Buff
    {
        public override decimal Durration { get { return 10m; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.DamageReduction)
                return 0.25m;

            return 0;
        }
    }
}