using System.Security.Cryptography;
using Ninject.Modules;

namespace CAHM.Wireup
{
    public class PasswordModule : NinjectModule 
    {
        public override void Load()
        {
            Kernel.Bind<HashAlgorithm>()
                  .To<SHA256Managed>();
        }
    }
}
