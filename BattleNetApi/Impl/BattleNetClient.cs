using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleNetApi.ApiSegments;

namespace BattleNetApi.Impl
{
    internal class BattleNetClient:IBattleNetClient
    {
        private IRestClient _restClient;

        public BattleNetClient(IRestClient restClient, IBattleNetConfiguration config)
        {
            _restClient = restClient;

            _restClient.BaseUrl = new Uri(config.ApiUrl);
            _restClient.DefaultParameters.Add(new Parameter { Type = ParameterType.QueryString, Name = "apiKey", Value = config.ApiKey });
            _restClient.DefaultParameters.Add(new Parameter { Type = ParameterType.QueryString, Name = "locale", Value = config.Locale });
        }

        private IBattleNetCharacter _character;

        public IBattleNetCharacter Character
        {
            get
            {
                if (_character == null)
                    _character = new BattleNetCharacter(_restClient);
                return _character;
            }
        }

        private IBattleNetGuild _guild;

        public IBattleNetGuild Guild
        {
            get
            {
                if (_guild == null)
                    _guild = new BattleNetGuild(_restClient);
                return _guild;
            }
        }

        private IBattleNetChallengeMode _challenge;

        public IBattleNetChallengeMode Challenge
        {
            get
            {
                if (_challenge == null)
                    _challenge = new BattleNetChallengeMode(_restClient);
                return _challenge;
            }
        }
    }
}
