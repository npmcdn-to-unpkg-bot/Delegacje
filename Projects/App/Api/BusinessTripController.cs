using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using CrazyAppsStudio.Delegacje.Tasks;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using CrazyAppsStudio.Delegacje.App.ApiBackend.Models;
using CrazyAppsStudio.Delegacje.Domain.Extensions;

namespace CrazyAppsStudio.Delegacje.App.Api
{
	[RoutePrefix("api/businessTrips")]
	[Authorize]
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
                    this.tasks.BusinessTripsTasks.CreateNewBusinessTrip(businessTrip, this.UserName);
                    return Ok("Delegacja została stworzona");
                }
                else
                {
                    return BadRequest(this.ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

		[Route("update")]
		[HttpPost]
		public IHttpActionResult UpdateBusinessTrip(BusinessTripDTO businessTrip)
		{
			try
			{
				BusinessTrip trip = this.tasks.BusinessTripsTasks.GetBusinessTrip(businessTrip.Id.Value);
				if (trip.UserId != this.User.Identity.GetUserId<int>())
				{
					return Unauthorized();
				}


				if (this.ModelState.IsValid)
				{
					if (!businessTrip.Id.HasValue)
						return BadRequest("Brak Id Delegacji");
					this.tasks.BusinessTripsTasks.UpdateBusinessTrip(businessTrip, this.UserName);
					return Ok("Delegacja została zaktualizowana");
				}
				else
				{
					return BadRequest(this.ModelState);
				}
			}
			catch (Exception ex)
			{
                return BadRequest(ex.Message);
            }
		}



        [Route("")]
        [HttpGet]
        public IEnumerable<BusinessTripSearchItemDTO> GetForUser()
        {			
            return tasks.BusinessTripsTasks.GetForUser(this.UserName);
        }
		
		[Route("{businessTripId:int}")]
		[HttpDelete]
		public IHttpActionResult Remove(int businessTripId)
		{
			BusinessTrip trip = this.tasks.BusinessTripsTasks.GetBusinessTrip(businessTripId);
			if (trip.UserId != this.User.Identity.GetUserId<int>())
			{
				return Unauthorized(); 
			}
			//TODO autoryzacja, user moze usuwac tylko wlasne delegacje
			tasks.BusinessTripsTasks.DeleteBusinessTrip(businessTripId);
			return Ok("Delegacja usunięta");
		}

		[Route("{businessTripId:int}")]
		[HttpGet]
		public IHttpActionResult GetById(int businessTripId)
		{
			BusinessTrip trip = this.tasks.BusinessTripsTasks.GetBusinessTrip(businessTripId);
			if (trip != null)
			{
				return Ok(trip.MapToDTO());
			}
			else return NotFound();			
		}

		[Route("clone/{businessTripId:int}")]
		[HttpGet]
		public BusinessTripSearchItemDTO Clone(int businessTripId)
		{			
			BusinessTrip trip = this.tasks.BusinessTripsTasks.GetBusinessTrip(businessTripId);

            BusinessTripDTO dto = trip.MapToDTO();
            dto.Title += " (kopia)";

            int createdId = this.tasks.BusinessTripsTasks.CreateNewBusinessTrip(dto, this.UserName);
			return this.tasks.BusinessTripsTasks.GetBusinessTrip(createdId).MapToSearchItem();			
		}
    }
}