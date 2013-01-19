using System.Web;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;

namespace CAHM.Wireup
{
    public class WebModule : NinjectModule 
    {
        public override void Load()
        {
            Kernel.Bind<HttpSessionStateBase>()
                  .ToMethod(ctx => ctx.Kernel.Get<HttpContextBase>().Session)
                  .InRequestScope();

        }
    }
}
