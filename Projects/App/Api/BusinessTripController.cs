using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Tasks;
using System;
using System.Collections.Generic;
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
        public IHttpActionResult CreateBusinessTrip(BusinessTripDTO businessTrip)
        {
            try
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
            catch
            {
                return InternalServerError();
            }
        }

		[Route("update")]
		[HttpPost]
		public IHttpActionResult UpdateBusinessTrip(BusinessTripDTO businessTrip)
		{
			try
			{
				if (this.ModelState.IsValid)
				{
					if (!businessTrip.Id.HasValue)
						return BadRequest("Brak Id Delegacji");
					this.tasks.BusinessTripsTasks.UpdateBusinessTrip(businessTrip);
					return Ok("Delegacja została zaktualizowana");
				}
				else
				{
					return BadRequest(this.ModelState);
				}
			}
			catch
			{
				return InternalServerError();
			}
		}

        [Route("")]
        [HttpGet]
        public IEnumerable<BusinessTripSearchItemDTO> GetForUser()
        {
            return tasks.BusinessTripsTasks.GetForUser(1);
        }
		
		[Route("{businessTripId:int}")]
		[HttpDelete]
		public IHttpActionResult Remove(int businessTripId)
		{
			//TODO autoryzacja, user moze usuwac tylko wlasne delegacje
			tasks.BusinessTripsTasks.DeleteBusinessTrip(businessTripId);
			return Ok("Delegacja usunięta");
		}
    }
}