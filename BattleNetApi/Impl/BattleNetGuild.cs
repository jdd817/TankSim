using BattleNetApi.ApiSegments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleNetApi.DataObjects;
using RestSharp;

namespace BattleNetApi.Impl
{
    public class BattleNetGuild : IBattleNetGuild
    {
        private IRestClient _restClient;

        public BattleNetGuild(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public GuildProfile GuildProfile(string realm, string guildName, params GuildFields[] fields)
        {
            var request = new RestRequest("character/{realm}/{guildName}");
            request.AddUrlSegment("realm", realm);
            request.AddUrlSegment("guildName", guildName);
            if (fields != null && fields.Length > 0)
                request.AddUrlSegment("fields", fields.Select(x => x.ToString()).Aggregate((a, b) => a + "," + b));

            var response = _restClient.Get<GuildProfile>(request);

            return response.Data;
        }

        public GuildProfile Achievements(string realm, string guildName)
        {
            return GuildProfile(realm, guildName, GuildFields.achievements);
        }

        public GuildProfile Challenge(string realm, string guildName)
        {
            return GuildProfile(realm, guildName, GuildFields.challenge);
        }

        public GuildProfile Members(string realm, string guildName)
        {
            return GuildProfile(realm, guildName, GuildFields.members);
        }

        public GuildProfile News(string realm, string guildName)
        {
            return GuildProfile(realm, guildName, GuildFields.news);
        }
    }
}
