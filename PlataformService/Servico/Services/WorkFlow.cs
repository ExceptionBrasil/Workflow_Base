using DAL.Workflow.Interfaces;
using Modelos.Infra;
using PlataformService.Servico.Interfaces;
using PlataformSrvice.Infraestrutura;
using Sessions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PlataformService.Servico.Services
{
    public class WorkFlow : IWorkFlow
    {
        private IWorkflowDAO workFlowDAO;

        public WorkFlow(IWorkflowDAO workFlowDAO)
        {
            this.workFlowDAO = workFlowDAO;
        }

        public async Task<bool> Run()
        {
            var workFlows = await workFlowDAO.GetWorkFlowsPendentes();            

            foreach (var workflow in workFlows)
            {
                var arquivosBase64 = await workFlowDAO.GetAttchments(workflow.Id);

               

                Email email = new Email();

                email.Anexos = new List<Attachment>();
                foreach (var arquivoBase64 in arquivosBase64)
                {
                    byte[] byteFile = Convert.FromBase64String(arquivoBase64.ImgBase64);
                    MemoryStream streamFile = new MemoryStream(byteFile);
                    Attachment attachment = new Attachment(streamFile, arquivoBase64.NomeOriginal);
                    email.Anexos.Add(attachment);
                }

                email.Body = workflow.CorpoEmail;
                email.To = workflow.EmailDestino;
                email.Subject = workflow.Assunto;

                EmailHelper emailManager = new EmailHelper(email);
                emailManager.Send();
                await workFlowDAO.WorkflowEnviado(workflow.Id);

            }
            return false;
        }
    }
}
