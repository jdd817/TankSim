﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank
{
    public interface IAbilityManager
    {
        T GetAbility<T>();
    }
}
