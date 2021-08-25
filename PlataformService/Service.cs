using Helpes.Infraestrutura;
using PlataformService.Servico.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PlataformSrvice
{
    public partial class Service : ServiceBase
    {

        private int intervaloDeExecucao = Convert.ToInt32(ConfigurationManager.AppSettings["ExecutarACada"]);
        private int daysToPurgTheLog = Convert.ToInt32(ConfigurationManager.AppSettings["PurgLog"]);

        private Timer TimerDeExcucao = new Timer();
        

        private static bool emExecucao = false;
        private IWorkFlow workFlow;

        /// <summary>
        /// Construtor do serviço
        /// </summary>
        /// <param name="_pedidosEmAberto"></param>
        public Service(IWorkFlow _workflow)
        {
            InitializeComponent();
            this.workFlow = _workflow;
        }

        /// <summary>
        /// Inicio do serviço 
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            TimerDeExcucao.Interval = intervaloDeExecucao;
            TimerDeExcucao.Elapsed += new ElapsedEventHandler(StartService);
            TimerDeExcucao.Enabled = true;
            TimerDeExcucao.AutoReset = true;

        }


        /// <summary>
        /// Inicialização do serviço
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void StartService(object sender, ElapsedEventArgs e)
        {
            EventoLog.RegisterAsync("WorkflowService", "Processo Iniciado");
            if (!emExecucao)
            {
                emExecucao = true;
                emExecucao = await workFlow.Run();

                EventoLog.RegisterAsync("WorkflowService", "Processo Finalizado");
                EventoLog.PurgAsync(daysToPurgTheLog);
            }
        }


        protected override void OnStop()
        {
            EventoLog.RegisterAsync("WorkflowService", "Serviço de execução do workflow parado.");
        }


    }
}
