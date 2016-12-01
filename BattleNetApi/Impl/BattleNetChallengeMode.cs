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
    internal class BattleNetChallengeMode : IBattleNetChallengeMode
    {
        private IRestClient _restClient;

        public BattleNetChallengeMode(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public IEnumerable<ChallengeMode> Realm(string realm)
        {
            var request = new RestRequest("challenge/{realm}");
            request.AddUrlSegment("realm", realm);

            var response = _restClient.Get<ChallengeModeWrapper>(request);

            return response.Data.challenge;
        }

        private class ChallengeModeWrapper
        {
            public List<ChallengeMode> challenge { get; set; }
        }
    }
}
