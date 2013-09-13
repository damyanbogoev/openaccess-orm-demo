using System;
using System.Web;
using Funq;
using MayLily.Data.OpenAccess;
using MayLily.DomainModel;
using MayLily.Infrastructure.Configuration;
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
            container.RegisterAutoWiredAs<Configuration, IConfiguration>().ReusedWithin(ReuseScope.Container);
            container.RegisterAutoWiredAs<MayLilyMetadataSource, IMetadataSource>().ReusedWithin(ReuseScope.Container);
            container.RegisterAutoWiredAs<MayLilyContext, IDbMigrator>().ReusedWithin(ReuseScope.None);
            container.RegisterAutoWiredAs<MayLilyContext, IMayLilyContext>().ReusedWithin(ReuseScope.None);
        }
    }

    public static class AppBootstrapper
    {
        public static void Start()
        {
            MayLilyAppHost appHost = new MayLilyAppHost();
            appHost.Init();

            IDbMigrator migrator = appHost.Container.Resolve<IDbMigrator>();
            migrator.MigrateSchema();
            migrator.SeedData();
        }
    }

    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AppBootstrapper.Start();
        }
    }
}
