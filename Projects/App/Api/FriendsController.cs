using CrazyAppsStudio.Delegacje.App.Api;
using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Tasks;
using System.Collections.Generic;
using System.Web.Http;

namespace CrazyAppsStudio.Delegacje.App.Controllers
{
    [RoutePrefix("Friends")]
    //[Authorize]
    public class FriendsController : BaseProfileController
    {
        private readonly ITasksRepository tasks;

        public FriendsController(ITasksRepository tasksService)
        {
            tasks = tasksService;
        }

        [Route("Search")]
        [HttpGet]
        public List<UserDetailsDTO> GetUserDetails()
        {
            return null;
            //return tasks.UsersTasks.;
        }
    }
}
