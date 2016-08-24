using CrazyAppsStudio.Delegacje.App.Models;
using CrazyAppsStudio.Delegacje.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using CrazyAppsStudio.Delegacje.DomainModel;
using CrazyAppsStudio.Delegacje.Domain.Entities.Identity;
using System;
using System.Net.Mail;
using System.Net;
using System.ComponentModel;
using CrazyAppsStudio.Delegacje.Tasks;
using Newtonsoft.Json.Linq;
using CrazyAppsStudio.Delegacje.App.Providers;
using CrazyAppsStudio.Delegacje.Domain.DTO;

namespace CrazyAppsStudio.Delegacje.App.Controllers
{
    [Authorize]
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private DelegacjeUserManager _userManager;
        private readonly ITasksRepository tasks;

        public AccountController()
        {
            this.tasks = new TasksRepository();
        }

		//public AccountController(DelegacjeUserManager userManager,
  //          ISecureDataFormat<AuthenticationTicket> accessTokenFormat, ITasksRepository tasks)
  //      {
  //          UserManager = userManager;
  //          AccessTokenFormat = accessTokenFormat;
  //          this.tasks = tasks;
  //      }

		public DelegacjeUserManager UserManager
        {
            get
            {
				return _userManager ?? Request.GetOwinContext().GetUserManager<DelegacjeUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        //// GET api/Account/UserInfo
        //[HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        //[Route("UserInfo")]
        //public UserInfoViewModel GetUserInfo()
        //{
        //    ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

        //    return new UserInfoViewModel
        //    {
        //        Email = User.Identity.GetUserName(),
        //        HasRegistered = externalLogin == null,
        //        LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
        //    };
        //}

        //// POST api/Account/Logout
        //[Route("Logout")]
        //public IHttpActionResult Logout()
        //{
        //    Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
        //    return Ok();
        //}

        //// GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        //[Route("ManageInfo")]
        //public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        //{
        //    IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

        //    if (user == null)
        //    {
        //        return null;
        //    }

        //    List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

        //    foreach (IdentityUserLogin linkedAccount in user.Logins)
        //    {
        //        logins.Add(new UserLoginInfoViewModel
        //        {
        //            LoginProvider = linkedAccount.LoginProvider,
        //            ProviderKey = linkedAccount.ProviderKey
        //        });
        //    }

        //    if (user.PasswordHash != null)
        //    {
        //        logins.Add(new UserLoginInfoViewModel
        //        {
        //            LoginProvider = LocalLoginProvider,
        //            ProviderKey = user.UserName,
        //        });
        //    }

        //    return new ManageInfoViewModel
        //    {
        //        LocalLoginProvider = LocalLoginProvider,
        //        Email = user.UserName,
        //        Logins = logins,
        //        ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
        //    };
        //}

        //// POST api/Account/ChangePassword
        //[Route("ChangePassword")]
        //public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
        //        model.NewPassword);

        //    if (!result.Succeeded)
        //    {
        //        return GetErrorResult(result);
        //    }

        //    return Ok();
        //}

        //// POST api/Account/SetPassword
        //[Route("SetPassword")]
        //public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

        //    if (!result.Succeeded)
        //    {
        //        return GetErrorResult(result);
        //    }

        //    return Ok();
        //}

        //// POST api/Account/AddExternalLogin
        //[Route("AddExternalLogin")]
        //public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

        //    AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

        //    if (ticket == null || ticket.Identity == null || (ticket.Properties != null
        //        && ticket.Properties.ExpiresUtc.HasValue
        //        && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
        //    {
        //        return BadRequest("External login failure.");
        //    }

        //    ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

        //    if (externalData == null)
        //    {
        //        return BadRequest("The external login is already associated with an account.");
        //    }

        //    IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
        //        new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

        //    if (!result.Succeeded)
        //    {
        //        return GetErrorResult(result);
        //    }

        //    return Ok();
        //}

        //// POST api/Account/RemoveLogin
        //[Route("RemoveLogin")]
        //public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    IdentityResult result;

        //    if (model.LoginProvider == LocalLoginProvider)
        //    {
        //        result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
        //    }
        //    else
        //    {
        //        result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
        //            new UserLoginInfo(model.LoginProvider, model.ProviderKey));
        //    }

        //    if (!result.Succeeded)
        //    {
        //        return GetErrorResult(result);
        //    }

        //    return Ok();
        //}

        //// GET api/Account/ExternalLogin
        //[OverrideAuthentication]
        //[HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        //[AllowAnonymous]
        //[Route("ExternalLogin", Name = "ExternalLogin")]
        //public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        //{
        //    if (error != null)
        //    {
        //        return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
        //    }

        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return new ChallengeResult(provider, this);
        //    }

        //    ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

        //    if (externalLogin == null)
        //    {
        //        return InternalServerError();
        //    }

        //    if (externalLogin.LoginProvider != provider)
        //    {
        //        Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
        //        return new ChallengeResult(provider, this);
        //    }

        //    ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
        //        externalLogin.ProviderKey));

        //    bool hasRegistered = user != null;

        //    if (hasRegistered)
        //    {
        //        Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

        //        ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
        //           OAuthDefaults.AuthenticationType);
        //        ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
        //            CookieAuthenticationDefaults.AuthenticationType);

        //        AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
        //        Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
        //    }
        //    else
        //    {
        //        IEnumerable<Claim> claims = externalLogin.GetClaims();
        //        ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
        //        Authentication.SignIn(identity);
        //    }

        //    return Ok();
        //}

        //// GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        //[AllowAnonymous]
        //[Route("ExternalLogins")]
        //public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        //{
        //    IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
        //    List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

        //    string state;

        //    if (generateState)
        //    {
        //        const int strengthInBits = 256;
        //        state = RandomOAuthStateGenerator.Generate(strengthInBits);
        //    }
        //    else
        //    {
        //        state = null;
        //    }

        //    foreach (AuthenticationDescription description in descriptions)
        //    {
        //        ExternalLoginViewModel login = new ExternalLoginViewModel
        //        {
        //            Name = description.Caption,
        //            Url = Url.Route("ExternalLogin", new
        //            {
        //                provider = description.AuthenticationType,
        //                response_type = "token",
        //                client_id = Startup.PublicClientId,
        //                redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
        //                state = state
        //            }),
        //            State = state
        //        };
        //        logins.Add(login);
        //    }

        //    return logins;
        //}

        // POST api/Account/Register
		[AllowAnonymous]
		[Route("register")]
		public async Task<IHttpActionResult> Register(RegisterBindingModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

            if (!model.MarketingAccepted)
            {
                return BadRequest("Proszę wyrazić zgodę na przetwarzanie danych osobowych.");
            }

			var user = new User() { UserName = model.Email, Email = model.Email, CompanyName = model.CompanyName, ActivationCode = Guid.NewGuid().ToString() };

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
			{
				return GetErrorResult(result);
			}

            var request = HttpContext.Current.Request;

            string siteUrl = string.Format("{0}://{1}{2}{3}",
                      request.Url.Scheme,
                      request.Url.Host,
                      request.Url.Port == 80
                        ? string.Empty : ":" + request.Url.Port,
                      request.ApplicationPath);

            if (!siteUrl.EndsWith("/"))
            {
                siteUrl += "/";
            }

            string callbackUrl = string.Format(siteUrl + "#/activateAccount?code={0}", user.ActivationCode);

            MailMessage message = new MailMessage();
            message.From = new MailAddress(@"noreply@claimit.delegacje.pl");
            message.To.Add(user.Email);
            message.Subject = "TEST";
            message.Body = "<b>Hi:</b>  <br/><br>Name: " + "<a href=\"" + callbackUrl + "\">" + callbackUrl + "</a>" +
                             "<br/><br>Message: " +
                             "<br/><br>";
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;
            SmtpClient smtp = new SmtpClient();
            smtp.Send(message);
            return Ok();
            //smtp.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);                        

            //return Ok(smtp.SendMailAsync(message));
		}

        //private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        //{
        //    return;
        //}

        [AllowAnonymous]
        [Route("activate")]
        [HttpPost]
        public async Task<IHttpActionResult> ActivateAccount([FromBody]string activationCode)
        {
            if (string.IsNullOrEmpty(activationCode))
            {
                return BadRequest("Kod aktywacyjny jest wymagany.");
            }

            User user = tasks.UsersTasks.GetUserByActivationCode(activationCode);

            if (user == null)
            {
                return BadRequest("Kod aktywacyjny już wykorzystany");
            }

            tasks.UsersTasks.ActivateUser(user.Id);
            
            return Ok(GenerateAccessTokenResponse(user));           
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> SendPasswordResetEmail(string emailAddress)
        {
            User user = await this.UserManager.FindByEmailAsync(emailAddress);

            if (user == null)
            {
                return BadRequest("Nieprawidłowy adres email");
            }

            string token = await this.UserManager.GeneratePasswordResetTokenAsync(user.Id);

            var request = HttpContext.Current.Request;

            string siteUrl = string.Format("{0}://{1}{2}{3}",
              request.Url.Scheme,
              request.Url.Host,
              request.Url.Port == 80
                ? string.Empty : ":" + request.Url.Port,
              request.ApplicationPath);
            if (!siteUrl.EndsWith("/"))
                siteUrl += "/";

            string callbackUrl = string.Format(
                    siteUrl + "#/resetPassword?userId={0}&passwordResetToken={1}",
                    user.Id,
                    HttpUtility.UrlEncode(token));

            MailMessage message = new MailMessage();
            message.From = new MailAddress(@"noreply@claimit.delegacje.pl");
            message.To.Add(emailAddress);
            message.Subject = "TEST";
            message.Body = "<b>Hi:</b>  <br/><br>Name: " + "<a href=\"" + callbackUrl + "\">" + callbackUrl + "</a>" +
                             "<br/><br>Message: " +
                             "<br/><br>";
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;
            SmtpClient smtp = new SmtpClient();
            smtp.Send(message);
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword")]
        public async Task<IHttpActionResult> ResetPassword(ResetPasswordDto resetPassword)
        {
            if (ModelState.IsValid)
            {
                User user = await this.UserManager.FindByIdAsync(resetPassword.UserId);
                if (user != null)
                {//if the user does not exist, still send success response
                    var result = await this.UserManager.ResetPasswordAsync(user.Id, resetPassword.Code, resetPassword.Password);
                    if (!result.Succeeded)
                    {
                        return GetErrorResult(result);
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok("Password has been successfully changed.");
        }

        private JObject GenerateAccessTokenResponse(User user)
        {

            var tokenExpiration = TimeSpan.FromDays(1);

            AuthenticationTicket ticket = ApplicationOAuthProvider.GenerateAuthenticationTicket(user, this.UserManager);

            var accessToken = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket);            

            JObject tokenResponse = new JObject(
                                        new JProperty("userName", user.UserName),                                        
                                        new JProperty("access_token", accessToken),
                                        new JProperty("token_type", "bearer"),
                                        new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString()),
                                        new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
                                        new JProperty(".expires", ticket.Properties.ExpiresUtc.ToString())                                        
            );

            return tokenResponse;
        }

        //// POST api/Account/RegisterExternal
        //[OverrideAuthentication]
        //[HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        //[Route("RegisterExternal")]
        //public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var info = await Authentication.GetExternalLoginInfoAsync();
        //    if (info == null)
        //    {
        //        return InternalServerError();
        //    }

        //    var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

        //    IdentityResult result = await UserManager.CreateAsync(user);
        //    if (!result.Succeeded)
        //    {
        //        return GetErrorResult(result);
        //    }

        //    result = await UserManager.AddLoginAsync(user.Id, info.Login);
        //    if (!result.Succeeded)
        //    {
        //        return GetErrorResult(result);
        //    }
        //    return Ok();
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        UserManager.Dispose();
        //    }

        //    base.Dispose(disposing);
        //}

        #region Helpers

        //private IAuthenticationManager Authentication
        //{
        //    get { return Request.GetOwinContext().Authentication; }
        //}

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        //private class ExternalLoginData
        //{
        //    public string LoginProvider { get; set; }
        //    public string ProviderKey { get; set; }
        //    public string UserName { get; set; }

        //    public IList<Claim> GetClaims()
        //    {
        //        IList<Claim> claims = new List<Claim>();
        //        claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

        //        if (UserName != null)
        //        {
        //            claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
        //        }

        //        return claims;
        //    }

        //    public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
        //    {
        //        if (identity == null)
        //        {
        //            return null;
        //        }

        //        Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

        //        if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
        //            || String.IsNullOrEmpty(providerKeyClaim.Value))
        //        {
        //            return null;
        //        }

        //        if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
        //        {
        //            return null;
        //        }

        //        return new ExternalLoginData
        //        {
        //            LoginProvider = providerKeyClaim.Issuer,
        //            ProviderKey = providerKeyClaim.Value,
        //            UserName = identity.FindFirstValue(ClaimTypes.Name)
        //        };
        //    }
        //}

        //private static class RandomOAuthStateGenerator
        //{
        //    private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

        //    public static string Generate(int strengthInBits)
        //    {
        //        const int bitsPerByte = 8;

        //        if (strengthInBits % bitsPerByte != 0)
        //        {
        //            throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
        //        }

        //        int strengthInBytes = strengthInBits / bitsPerByte;

        //        byte[] data = new byte[strengthInBytes];
        //        _random.GetBytes(data);
        //        return HttpServerUtility.UrlTokenEncode(data);
        //    }
        //}

        #endregion
    }
}
