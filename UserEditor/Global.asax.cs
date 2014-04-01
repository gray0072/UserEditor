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

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

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
            unity.RegisterType<IUsersService, UsersService>();
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