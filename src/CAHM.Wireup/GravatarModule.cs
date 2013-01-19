using System.Security.Cryptography;
using Ninject.Modules;

namespace CAHM.Wireup
{
    public class GravatarModule : NinjectModule 
    {
        public override void Load()
        {
            Kernel.Bind<MD5>()
                  .To<MD5CryptoServiceProvider>();
        }
    }
}
