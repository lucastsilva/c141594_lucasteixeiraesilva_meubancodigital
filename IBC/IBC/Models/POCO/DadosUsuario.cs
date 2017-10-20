using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBC.Models
{
    public class DadosUsuario
    {
        public string nome { get; set; }
        public string dataUltimoAcesso { get; set; }
        public string horaUltimoAcesso { get; set; }
        public string segmento { get; set; }
    }
}