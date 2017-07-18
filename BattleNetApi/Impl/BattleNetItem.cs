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
    internal class BattleNetItem : IBattleNetItem
    {
        private IRestClient _restClient;

        public BattleNetItem(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public Item Item(int itemId, params int[] bonusList)
        {
            var request = new RestRequest("item/{itemId}");
            request.AddUrlSegment("itemId", itemId.ToString());
            if (bonusList != null && bonusList.Length > 0)
                request.AddQueryParameter("bl", bonusList.Select(x => x.ToString()).Aggregate((a, b) => a + "," + b));

            var response = _restClient.Get<Item>(request);

            return response.Data;
        }
    }
}
