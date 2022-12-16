using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Common
{
    public class ConfigHelper
    {
        public static T Get<T>(string key)
        {
            return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));  //web.config de yazdıklarımızı okuması için bunu yazıyoruz bunu da mail helperda yazıyoruz orada getirsin diye
        }
    }
}
