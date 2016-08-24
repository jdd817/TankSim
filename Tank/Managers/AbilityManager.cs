using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Managers
{
    public class AbilityManager : IAbilityManager
    {
        List<Abilities.Ability> _abilites;

        public AbilityManager(List<Abilities.Ability> abilities)
        {
            _abilites = abilities;
        }

        public T GetAbility<T>()
        {
            return _abilites.OfType<T>().FirstOrDefault();
        }
    }
}
