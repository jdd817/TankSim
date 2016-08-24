namespace Tank
{
    public interface ICombatEngine
    {
        IRng Rng { get; set; }
        decimal TimeIncrement { get; set; }

        void DoCombat(Player Tank, Mob Mob, decimal Durration, bool EndAtDeath = false, Healer[] Healers = null);
    }
}