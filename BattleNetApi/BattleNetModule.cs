using Ninject.Modules;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi
{
    public class BattleNetModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IBattleNetClient>().To<Impl.BattleNetClient>()
                .WithConstructorArgument("restClient", _ => new RestSharp.RestClient());
        }
    }
}
