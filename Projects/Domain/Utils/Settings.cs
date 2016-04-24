using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Domain.Utils
{
    public class Settings
    {
        public static bool EnforceHTTPS
        {
            get
            {
                return ConfigurationManager.AppSettings["EnforceHTTPS"] == null ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["EnforceHTTPS"]);
            }
        }
    }
}
