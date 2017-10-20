using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBC.Models
{
    public class Saldo
    {
        public string saldo { get; set; }
        public object saldoTotal { get; set; }
        public object msgErro { get; set; }
        public object chaveResource { get; set; }
        public object nomeTitular { get; set; }
        public object cpfCnpj { get; set; }
        public string sinalSaldo { get; set; }
        public object limite { get; set; }
        public object saldoBloqueado { get; set; }
        public object saldoDisponivelComLimite { get; set; }
        public object temRascunho { get; set; }
        public bool outorgado { get; set; }
        public bool visivel { get; set; }
    }
}