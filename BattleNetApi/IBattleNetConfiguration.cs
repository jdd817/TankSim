using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi
{
    public interface IBattleNetConfiguration
    {
        string ApiUrl { get; set; }
        string ApiKey { get; set; }
        string Locale { get; set; }
    }
}
