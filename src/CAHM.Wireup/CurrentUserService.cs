using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace CAHM.Wireup
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly HttpContextBase _context;

        public CurrentUserService(HttpContextBase context)
        {
            _context = context;
        }

        public string Email { get { return IdentityName(); } }

        private IPrincipal User()
        {
            return _context.User;
        }

        private IIdentity Identity()
        {
            var user = User();
            return user == null ? null : user.Identity;
        }

        private string IdentityName()
        {
            var identity = Identity();
            if (identity == null || !identity.IsAuthenticated) return null;
            return identity.Name;
        }


    }
}
