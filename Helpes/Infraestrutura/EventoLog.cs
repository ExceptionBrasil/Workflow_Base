using Dapper;
using Sessions.Infraestrutura;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Helpes.Infraestrutura
{
    public static class EventoLog
    {
        /// <summary>
        /// Registra o log
        /// </summary>
        /// <param name="process"></param>
        /// <param name="logMessage"></param>
        public static  void RegisterAsync(string process, string logMessage)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.connectionStringTOTVS))
            {
                connection.Open();
                try
                {
                    var rowsAffects =  connection.Execute("Insert into ServiceLog (Process,LogMessage) " +
                                                                 "values (@Process,@LogMessage)",
                                                                 new
                                                                 {
                                                                     Process = process,
                                                                     LogMessage = logMessage
                                                                 },commandTimeout:3000);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
        /// <summary>
        /// Exclui log maior que 5 Dias
        /// </summary>
        public static  void  PurgAsync(int daysToPurg)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.connectionStringTOTVS))
            {
                connection.Open();
                try
                {
                    var rowsAffects =  connection.Execute($@"delete ServiceLog where DataLog<GetDate()-{daysToPurg} ", commandTimeout: 3000);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
