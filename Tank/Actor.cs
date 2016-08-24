using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank
{
    public interface Actor
    {
        string Name
        { get; set; }
        decimal DodgeChance { get; }
        decimal ParryChance { get; }
        decimal BlockChance { get; }
        decimal MissChance { get; }
        decimal CritChance { get; }

        IBuffManager Buffs { get; }

        List<Weapon> Weapons
        { get; set; }

        Abilities.Attack GetAttack();

        void UpdateTimeElapsed(decimal DeltaTime);

        int MaxHealth { get; set; }
        int CurrentHealth { get; set; }

        void Reset();
    }
}
