using System.Linq;
using CAHM.Raven;
using Ninject.Modules;

namespace CAHM.Wireup
{
    public class ServiceModule : NinjectModule 
    {
        public override void Load()
        {
            var types = new[] {typeof (ILogInAccounts), typeof (LogInAccounts)}
                .Select(type => type.Assembly)
                .SelectMany(assembly => assembly.GetTypes())
                .ToArray();

            var services = types.Where(t => t.IsInterface && !t.IsGenericTypeDefinition);

            var implementations = types.Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericTypeDefinition);

            services.SelectMany(t => implementations, (service, impl) => new {service, impl})
                    .Where(x => x.service.IsAssignableFrom(x.impl))
                    .GroupBy(x => x.service, x => x.impl)
                    .Where(g => g.Count() == 1)
                    .Select(g => new {service = g.Key, impl = g.Single()})
                    .ToList()
                    .ForEach(x => Kernel.Bind(x.service).To(x.impl));
        }
    }
}
