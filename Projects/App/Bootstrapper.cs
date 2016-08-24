using CrazyAppsStudio.Delegacje.App.Controllers;
using CrazyAppsStudio.Delegacje.Repository;
using CrazyAppsStudio.Delegacje.Tasks;
using Microsoft.Practices.Unity;
using System.Web.Http;
using System.Web.Mvc;
using Unity.Mvc4;

namespace CrazyAppsStudio.Delegacje.App
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            container.RegisterInstance(typeof(HttpConfiguration), GlobalConfiguration.Configuration);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();    
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ITasksRepository, TasksRepository>();
            container.RegisterType<IRepositories, Repositories>();

            //this line is quite a mystery but apparently we need to register account related controllers like that to make them wotk with Unity DI            
            container.RegisterType<AccountController>(new InjectionConstructor());
        }
    }
}