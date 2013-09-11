using System;
using System.Collections.Generic;
using System.Configuration;
using Telerik.OpenAccess;

namespace MayLily.Infrastructure.Configuration
{
    public class Configuration : IConfiguration
    {
        private static readonly Dictionary<string, Func<object>> SettingsTrunk = new Dictionary<string, Func<object>>();

        public string ConnectionString
        {
            get
            {
                return Configuration.ReadConnectionString();
            }
        }

        private static string ReadConnectionString()
        {
            string connectionStringName = Configuration.ReadSettings(Constants.ConnectionStringId);
            if (string.IsNullOrEmpty(connectionStringName))
            {
                connectionStringName = Constants.DefaultConnectionStringId;
            }

            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (connectionString == null)
            {
                return null;
            }

            return connectionString.ConnectionString;
        }

        public BackendConfiguration BackendConfiguration
        {
            get
            {
                return Configuration.ReadBackendConfiguration();
            }
        }

        private static BackendConfiguration ReadBackendConfiguration()
        {
            string configName = Configuration.ReadSettings(Constants.BackendConfigurationName) ?? Constants.DefaultBackendConfigurationName;
            BackendConfiguration result = new BackendConfiguration { Backend = "mssql", ProviderName = "System.Data.SqlClient" };
            BackendConfiguration.MergeBackendConfigurationFromConfigFile(result, ConfigurationMergeMode.ConfigFileDefinitionWins, configName);

            return result;
        }

        private static string ReadSettings(string key)
        {
            return Configuration.ReadSettings(key, value => value);
        }

        private static TValue ReadSettings<TValue>(string key, Func<string, TValue> getValue)
        {
            if (Configuration.SettingsTrunk.ContainsKey(key) == false)
            {
                Func<object> valueGetter = () =>
                {
                    var value = ConfigurationManager.AppSettings[key];
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        value = null;
                    }

                    return getValue(value);
                };

                Configuration.SettingsTrunk.Add(key, valueGetter);
            }

            return (TValue)Configuration.SettingsTrunk[key].Invoke();
        }
    }
}
