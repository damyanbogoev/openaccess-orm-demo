using System;
using System.Web;
using Funq;
using MayLily.Data.OpenAccess;
using MayLily.DomainModel;
using MayLily.Infrastructure.Configuration;
using MayLily.Infrastructure.Data;
using ServiceStack.WebHost.Endpoints;

namespace MayLily.Services
{
    public class MayLilyAppHost : AppHostBase
    {
        public MayLilyAppHost()
            : base("OpenAccess ORM and ServiceStack integration sample", typeof(MayLilyAppHost).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            container.Adapter = new DependencyManager();
        }
    }

    public static class AppBootstrapper
    {
        public static void Start()
        {
            IDbMigrator migrator = DependencyManager.Resolve<IDbMigrator>(verify: true);
            migrator.MigrateSchema();
            migrator.SeedData();

            new MayLilyAppHost().Init();
        }
    }

    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AppBootstrapper.Start();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}