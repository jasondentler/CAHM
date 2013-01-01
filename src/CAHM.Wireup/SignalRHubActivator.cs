using System;
using Microsoft.AspNet.SignalR.Hubs;
using Ninject;

namespace CAHM.Wireup
{
    public class SignalRHubActivator : IHubActivator
    {
        private readonly IKernel _kernel;

        public SignalRHubActivator(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IHub Create(HubDescriptor descriptor)
        {
            if (descriptor == null)
            {
                throw new ArgumentNullException("descriptor");
            }

            if (descriptor.HubType == null)
            {
                return null;
            }

            return _kernel.TryGet(descriptor.HubType) as IHub;
        }
    }
}