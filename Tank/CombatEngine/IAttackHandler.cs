using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.CombatEngine
{
    public interface IAttackHandler
    {
        void ProcessAttack(Player Tank, Mob Mob, decimal Time, Attack PlayerAttack);
    }
}
