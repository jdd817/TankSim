using Ninject;
using Ninject.MockingKernel.NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankTests
{
    public abstract class TestBase : IDisposable
    {
        protected IKernel Kernel;

        protected DateTime CurrentDate;

        protected TestBase()
        {
            Kernel = new NSubstituteMockingKernel();
            Kernel.Bind<string>().ToConstant("");

            CurrentDate = DateTime.Now;

            SetBindings();
        }

        protected virtual void SetBindings()
        { }

        public virtual void Dispose()
        {
            if (Kernel != null)
                Kernel.Dispose();
        }
    }

    public abstract class TestBase<T> : TestBase
    {
        protected T Service;

        protected TestBase()
            : base()
        {
            Service = Kernel.Get<T>();
        }

        public override void Dispose()
        {
            base.Dispose();
            var disposableService = Service as IDisposable;
            if (disposableService != null)
                disposableService.Dispose();
        }
    }

    public static class Extensions
    {
        public static T Do<T>(this T obj, Action<T> action)
        {
            action(obj);
            return obj;
        }
    }
}
