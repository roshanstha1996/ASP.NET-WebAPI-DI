using System.Web.Http;
using Unity;
using Unity.WebApi;
using Repository;

namespace Aastha_WebAPI_DI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            //whenever we call repo class, instanceof server class is automatically created
            container.RegisterType <IProductRepo, ProductRepo> ();
            container.RegisterType <IUserRepo, UserRepo> ();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}