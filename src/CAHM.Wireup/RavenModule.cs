using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAHM.Raven;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace CAHM.Wireup
{
    public class RavenModule : NinjectModule 
    {
        public override void Load()
        {
            var store = new DocumentStore()
            {
                ConnectionStringName = "RavenDB"
            }.Initialize();

            CreateIndices(store);

            Kernel.Bind<IDocumentStore>()
                  .ToConstant(store);

            Kernel.Bind<IDocumentSession>()
                  .ToMethod(ctx => ctx.Kernel.Get<IDocumentStore>().OpenSession())
                  .InRequestScope();
        }

        private static void CreateIndices(IDocumentStore store)
        {
            IndexCreation.CreateIndexes(typeof (LogInAccounts).Assembly, store);
        }
    }
}
