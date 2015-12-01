using System.Net.Http.Headers;
using System.Web.Http;

namespace CrazyAppsStudio.Delegacje.App.Api
{
    public class BaseProfileController : ApiController
    {
        public string UserName
        {
            get
            {
              AuthenticationHeaderValue authHeader = this.Request.Headers.Authorization;
              return authHeader.Parameter;

                //var user = this.User.Identity;
                //if (user != null)
                //    return user.Name;
                //else
                //    throw new AccessViolationException("Error reading token data");
            }
        }
    }
}