using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;

namespace UserEditor
{
    using System.Data.Entity.Migrations;
    using System.Web.Mvc;
    using Microsoft.Practices.Unity;
    using UserEditor.IoC;
    using UserEditor.Migrations;
    using UserEditor.Models;
    
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AuthConfig.RegisterAuth();

            this.UpdateDatabase();
            
            var unity = new UnityContainer();
            //unity.RegisterType<IUsersService, UsersDbService>();
            unity.RegisterType<IUsersService, UsersXmlService>(new InjectionConstructor(Server.MapPath(@"~\App_Data\Users.xml")));
            GlobalConfiguration.Configuration.DependencyResolver = new UnityResolver(unity);
		}

	    private void UpdateDatabase()
	    {
            Configuration configuration = new Configuration();
            configuration.ContextType = typeof(UsersDB);
            var migrator = new DbMigrator(configuration);

            //This will run the migration update script and will run Seed() method
            migrator.Update();
	    }
	}
}