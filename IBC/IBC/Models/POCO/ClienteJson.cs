using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBC.Models
{
    public class Conta
    {
        public string agencia { get; set; }
        public string tipo { get; set; }
        public string chaveRecurso { get; set; }
        public string conta { get; set; }
        public string nome { get; set; }
        public object siglaConta { get; set; }
        public bool selecionada { get; set; }
        public string chaveServidor { get; set; }
        public string cpf { get; set; }
    }

    public class ContaCliente
    {
        public bool temContasPF { get; set; }
        public bool temContasPJ { get; set; }
        public bool temContasGOV { get; set; }
        public int segmento { get; set; }
        public IList<Conta> contas { get; set; }
    }
}