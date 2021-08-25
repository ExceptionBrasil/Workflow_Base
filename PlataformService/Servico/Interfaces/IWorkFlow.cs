using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformService.Servico.Interfaces
{
    public interface IWorkFlow
    {
        Task<bool> Run();
    }
}
