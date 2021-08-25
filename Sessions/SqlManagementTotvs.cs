using Sessions;
using Sessions.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionControl
{
    public class SqlManagementTotvs: ISqlManagementTotvs
    {
        public SqlManagementTotvs()
        {
            this.Conecxao = GetConnectionTOTVS();
        }

        public SqlConnection Conecxao { get; set; }

        private static SqlConnection GetConnectionTOTVS()
        {
            SqlConnection conn = new SqlConnection(SqlHelper.connectionStringTOTVS);
            conn.Open();
            return conn;
        }


    }
}
