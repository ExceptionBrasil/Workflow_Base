using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RHChamados.Modelos.Tickets
{
    public class ImagensTickets
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeOriginal { get; set; }
        public string Descricao { get; set; }

        public DateTime DataInclusao { get; set; }

        public bool Ativo { get; set; }

        public string ImgBase64 { get; set; }

        public string PathServer { get; set; }
        public string RelativePath { get; set; }

        public string Extensao { get; set; }
        public long Tamanho { get; set; }

        public string ZC0_NUM { get; set; }
    }

}
