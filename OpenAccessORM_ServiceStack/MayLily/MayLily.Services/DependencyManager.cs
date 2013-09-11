using System;
using System.Linq;
using MayLily.Data.OpenAccess;
using MayLily.DomainModel;
using MayLily.Infrastructure.Configuration;
using MayLily.Infrastructure.Data;
using Microsoft.Practices.Unity;
using ServiceStack.Configuration;

namespace MayLily.Services
{
    public class DependencyManager : IContainerAdapter
    {
        private static readonly IUnityContainer container;

        static DependencyManager()
        {
            DependencyManager.container = new UnityContainer();

            DependencyManager.container.RegisterType<IConfiguration, Configuration>();
            DependencyManager.container.RegisterType<IMetadataSource, MayLilyMetadataSource>();
            DependencyManager.container.RegisterType<IDbMigrator, MayLilyContext>();
            DependencyManager.container.RegisterType<IMayLilyContext, MayLilyContext>();
        }

        public static T Resolve<T>(bool verify = false)
        {
            T result = DependencyManager.TryResolve<T>();
            if (verify == true && result == null)
            {
                throw new InvalidOperationException(string.Format("Unable to find registered dependency for '{0}'.", typeof(T).FullName));
            }

            return result;
        }

        public static T TryResolve<T>()
        {
            if (DependencyManager.container.IsRegistered<T>())
            {
                return DependencyManager.container.Resolve<T>();
            }

            return default(T);
        }

        T IContainerAdapter.Resolve<T>()
        {
            return DependencyManager.Resolve<T>();
        }

        T IContainerAdapter.TryResolve<T>()
        {
            return DependencyManager.TryResolve<T>();
        }
    }
}