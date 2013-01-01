using System.Web.Mvc;
using Raven.Client;

namespace CAHM.Wireup
{
    public class RavenGlobalFilter : IActionFilter
    {
        private readonly IDocumentSession _session;

        public RavenGlobalFilter(IDocumentSession session)
        {
            _session = session;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!filterContext.IsChildAction && filterContext.Exception == null)
            {
                _session.SaveChanges();
            }
            _session.Dispose();
        }
    }
}