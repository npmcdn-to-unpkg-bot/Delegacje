using CrazyAppsStudio.Delegacje.Repository;
using CrazyAppsStudio.Delegacje.Tasks;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace CrazyAppsStudio.Delegacje.App
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            container.RegisterType<ITasksRepository, TasksRepository>();
            container.RegisterType<IRepositories, Repositories>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}