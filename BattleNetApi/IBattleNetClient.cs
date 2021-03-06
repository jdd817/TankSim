﻿using BattleNetApi.ApiSegments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi
{
    public interface IBattleNetClient
    {
        IBattleNetGuild Guild { get; }
        IBattleNetCharacter Character { get; }
        IBattleNetChallengeMode Challenge { get; }
        IBattleNetItem Item { get; }
    }
}
