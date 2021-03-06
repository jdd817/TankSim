﻿using BattleNetApi.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi.ApiSegments
{
    public interface IBattleNetChallengeMode
    {
        IEnumerable<ChallengeMode> Realm(string realm);
    }
}
