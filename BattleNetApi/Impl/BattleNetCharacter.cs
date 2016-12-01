using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleNetApi.ApiSegments;
using BattleNetApi.DataObjects;
using RestSharp;

namespace BattleNetApi.Impl
{
    internal class BattleNetCharacter : IBattleNetCharacter
    {
        private IRestClient _restClient;

        public BattleNetCharacter(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public Character CharacterProfile(string realm, string characterName, params CharacterFields[] fields)
        {
            var request = new RestRequest("character/{realm}/{characterName}");
            request.AddUrlSegment("realm", realm);
            request.AddUrlSegment("characterName", characterName);
            if (fields != null && fields.Length > 0)
                request.AddUrlSegment("fields", fields.Select(x=>x.ToString()).Aggregate((a, b) => a + "," + b));

            var response = _restClient.Get<Character>(request);

            return response.Data;
        }

        public Character Achievements(string realm, string characterName)
        {
            return CharacterProfile(realm, characterName, CharacterFields.achievements);
        }

        public Character Appearance(string realm, string characterName)
        {
            return CharacterProfile(realm, characterName, CharacterFields.appearance);
        }

        public Character Feed(string realm, string characterName)
        {
            return CharacterProfile(realm, characterName, CharacterFields.feed);
        }

        public Character Guild(string realm, string characterName)
        {
            return CharacterProfile(realm, characterName, CharacterFields.guild);
        }

        public Character Items(string realm, string characterName)
        {
            return CharacterProfile(realm, characterName, CharacterFields.items);
        }

        public Character Stats(string realm, string characterName)
        {
            return CharacterProfile(realm, characterName, CharacterFields.stats);
        }
    }
}
