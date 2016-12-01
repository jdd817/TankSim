using BattleNetApi.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi.ApiSegments
{
    public interface IBattleNetGuild
    {
        GuildProfile GuildProfile(string realm, string guildName, params GuildFields[] fields);
        GuildProfile Members(string realm, string guildName);
        GuildProfile Achievements(string realm, string guildName);
        GuildProfile News(string realm, string guildName);
        GuildProfile Challenge(string realm, string guildName);
    }
}

namespace BattleNetApi
{ 
    public enum GuildFields
    {
        members,
        achievements,
        news,
        challenge
    }
}
