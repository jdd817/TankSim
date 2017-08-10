namespace Tank.Buffs.Druid
{
    public class LunarBeam : HealOverTime
    {
        public LunarBeam(int healingPerTick)
        {
            HealingPerTick = healingPerTick;
        }

        public override decimal Durration { get { return 8m; } }
    }
}