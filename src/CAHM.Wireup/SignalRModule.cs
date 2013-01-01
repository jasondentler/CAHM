using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Ninject;
using Ninject.Modules;

namespace CAHM.Wireup
{
    public class SignalRModule : NinjectModule 
    {
        public override void Load()
        {
            Kernel.Bind<IHubActivator>().ToConstant(new SignalRHubActivator(Kernel));
            GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => Kernel.Get<IHubActivator>());       
        }
    }
}


