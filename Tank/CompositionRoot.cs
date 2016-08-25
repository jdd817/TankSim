using Ninject.Modules;
using Ninject.Extensions.Conventions;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.CombatEngine;

namespace Tank
{
    public class CompositionRoot : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind(x => x
                .FromThisAssembly()
                .SelectAllClasses()
                .BindAllInterfaces());

            foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "Tank.Classes"))
            {
                Kernel.Bind(x =>
                    x.FromThisAssembly()
                    .Select(t => t.Namespace == "Tank.Abilities." + type.Name)
                    .BindBase()
                    .Configure(cfg => cfg.When(req =>
                        req.ParentRequest != null &&
                            req.ParentRequest.Target.Member.DeclaringType == type
                    )));
            }

            Kernel.Rebind<IRng>().To<RNG>().InThreadScope();

            Kernel.Rebind<IAttackHandler>().To<PlayerAttackHandler>().When(req => req.Target.Name.ToLower().Contains("player"));
            Kernel.Bind<IAttackHandler>().To<MobAttackHandler>().When(req => req.Target.Name.ToLower().Contains("mob"));
        }
    }
}
