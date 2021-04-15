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

            //whenever we call repo class, instance of server repository class is automatically created here
            container.RegisterType <IProductRepo, ProductRepo> ();
            container.RegisterType <IUserRepo, UserRepo> ();
            container.RegisterType<ISalesRepo, SalesRepo>();
            container.RegisterType<IBillRepo, BillRepo>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}