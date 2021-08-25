using SimpleInjector;
using System.ServiceProcess;
using SessionControl;
using Sessions;
using PlataformService.Servico.Services;
using PlataformService.Servico.Interfaces;
using DAL.Workflow.DAO;
using DAL.Workflow.Interfaces;

namespace PlataformSrvice
{
    static class Program
    {

        static Container container;

        static Program()
        {
            container = new Container();

            container.Register<ISqlManagementTotvs, SqlManagementTotvs>(Lifestyle.Singleton);
            container.Register<IWorkflowDAO, WorkflowDAO>(Lifestyle.Singleton);
            container.Register<IWorkFlow, WorkFlow>(Lifestyle.Singleton);
            container.Verify();
        }

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        static void Main()
        {
             var workFlow = container.GetInstance<IWorkFlow>();



            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service(workFlow)
            };

            ServiceBase.Run(ServicesToRun);
        }
    }
}
