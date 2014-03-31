using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace UserEditor
{
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Model;

    using UserEditor.Migrations;

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