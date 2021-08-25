using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sessions.Infraestrutura
{
    public static class SqlHelper
    {
        public static string connectionStringTOTVS = ConfigurationManager.ConnectionStrings["ConnectionStringTOTVS"].ToString();
    }
}
