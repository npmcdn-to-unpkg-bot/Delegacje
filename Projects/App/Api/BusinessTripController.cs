using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Tasks;
using System.Web.Http;

namespace CrazyAppsStudio.Delegacje.App.Api
{
	[RoutePrefix("api/businessTrips")]
	//[Authorize]
	public class BusinessTripController : BaseProfileController
    {
		private readonly ITasksRepository tasks;

		public BusinessTripController(ITasksRepository tasks)
        {
            this.tasks = tasks;
        }

		[Route("create")]
		[HttpPut]
		public IHttpActionResult CreateBusinessTrip(AddBusinessTripDTO businessTrip)
        {
			if (this.ModelState.IsValid)
			{
				this.tasks.BusinessTripsTasks.CreateNewBusinessTrip(businessTrip);
				return Ok("Delegacja została stworzona");
			}
			else
			{
				return BadRequest(this.ModelState);
			}			
        }
    }
}