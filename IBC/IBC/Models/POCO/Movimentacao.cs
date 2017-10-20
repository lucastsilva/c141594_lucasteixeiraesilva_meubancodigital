using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBC.Models
{
    public class Movimentacao
    {
        public string dt_movimentacao { get; set; }
        public string nr_doc { get; set; }
        public string historico { get; set; }
        public string valor { get; set; }
        public string saldo { get; set; }
    }
}