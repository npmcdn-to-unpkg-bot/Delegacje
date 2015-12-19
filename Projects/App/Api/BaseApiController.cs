using CrazyAppsStudio.Delegacje.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CrazyAppsStudio.Delegacje.App.Api
{
    public class BaseApiController : ApiController
    {
		/// <summary>
		/// Database context
		/// </summary>
		private readonly DelegacjeContext _databaseContext;

		/// <summary>
		/// ASP.NET Identity user manager
		/// </summary>
		private readonly DelegacjeUserManager _userManager;


		public DelegacjeContext DatabaseContext
		{
			get { return _databaseContext; }
		} 

		public DelegacjeUserManager UserManager
		{
			get { return _userManager; }
		} 

		public BaseApiController()
		{
			this._databaseContext = new DelegacjeContext();
			this._userManager = DelegacjeUserManager.Create(this.DatabaseContext);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.UserManager.Dispose();
				this.DatabaseContext.Dispose();
			}

			base.Dispose(disposing);
		}
    }
}