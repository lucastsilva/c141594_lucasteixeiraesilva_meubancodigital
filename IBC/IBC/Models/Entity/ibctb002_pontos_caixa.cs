namespace IBC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ibctb002_pontos_caixa
    {
        [Key]
        public int co_pontos_caixa { get; set; }

        public int co_cliente { get; set; }

        public int nu_pontos_caixa { get; set; }

        public DateTime dt_movimentacao { get; set; }

        public bool ic_movimentacao { get; set; }

        public DateTime? dt_expiracao { get; set; }

        public virtual ibctb001_cliente ibctb001_cliente { get; set; }
    }
}
