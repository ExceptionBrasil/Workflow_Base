using Modelos.Workflow;
using RHChamados.Modelos.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Workflow.Interfaces
{
    public interface IWorkflowDAO
    {
        Task<IEnumerable<WorkFlow>> GetWorkFlowsPendentes();
        Task<IEnumerable<ImagensTickets>> GetAttchments(int id);
        Task<bool> WorkflowEnviado(int id);
    }
}
