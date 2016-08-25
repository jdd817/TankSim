using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.CombatEngine
{
    public interface IAbilityHandler
    {
        void ProcessAction(Player Tank, Mob Mob, decimal Time, Ability PlayerAction);
    }
}
