using CrazyAppsStudio.Delegacje.App.Api;
using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Tasks;
using System.Web.Http;

namespace CrazyAppsStudio.Delegacje.App.Controllers
{
    [RoutePrefix("api/dictionaries")]
    [Authorize]
    public class DictionariesController : BaseProfileController
    {
        private readonly ITasksRepository tasks;

        public DictionariesController(ITasksRepository tasks)
        {
            this.tasks = tasks;
        }

        [Route("")]
        [HttpGet]
        public DictionariesDTO GetDictionaries()
        {
            return tasks.DictionariesTasks.GetDictionaries();
        }
    }
}
