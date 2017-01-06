using Ninject.Modules;
using Ninject.Extensions.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.ItemOptimization
{
    public class ItemOptimizationModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind(x => x
                .FromThisAssembly()
                .SelectAllClasses()
                .BindAllInterfaces());
        }
    }
}
