using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace FromSqlError
{
    public class ConfigurationAccessor
    {
        private static IConfigurationRoot _config;

        public static IConfigurationRoot Config {
            get
            {
                if (_config == null)
                {
                    var builder = new ConfigurationBuilder();
                    builder.SetBasePath(AppContext.BaseDirectory);
                    builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    builder.AddJsonFile("appsettings.unversioned.json", optional: true, reloadOnChange: false);
                    _config = builder.Build();
                }

                return _config;
                
            }
        }
    }
}
