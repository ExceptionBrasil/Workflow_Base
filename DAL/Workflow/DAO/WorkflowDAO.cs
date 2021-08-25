using DAL.Workflow.Interfaces;
using Dapper;
using Modelos.Workflow;
using RHChamados.Modelos.Tickets;
using Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Workflow.DAO
{
    public class WorkflowDAO : IWorkflowDAO
    {
        private ISqlManagementTotvs session;

        public WorkflowDAO(ISqlManagementTotvs sql)
        {
            this.session = sql;
        }

        public async Task<IEnumerable<ImagensTickets>> GetAttchments(int id)
        {
            IEnumerable<ImagensTickets> attachments = await this.session
                                                .Conecxao
                                                .QueryAsync<ImagensTickets>($"Exec GetAttachments {id}",
                                                 commandType: System.Data.CommandType.Text, commandTimeout: 3000);
            return attachments;
        }

        public async Task<IEnumerable<WorkFlow>> GetWorkFlowsPendentes()
        {
            IEnumerable<WorkFlow> workflows = await this.session
                                                .Conecxao                                                
                                                .QueryAsync<WorkFlow>("Exec GetWorkflows",
                                                 commandType: System.Data.CommandType.Text, commandTimeout: 3000);
            return workflows;
        }

        public async Task<bool> WorkflowEnviado(int id)
        {
           var success =  await this.session.Conecxao
                        .ExecuteAsync("update WorkFlow set EnvioOK=1, DataEnvio=@DataEnviada where Id=@Id",
                                                                 new
                                                                 {
                                                                     Id = id,
                                                                     DataEnviada = DateTime.Now
                                                                 },commandTimeout:3000);
            return success > 0;
                
        }
    }
}
