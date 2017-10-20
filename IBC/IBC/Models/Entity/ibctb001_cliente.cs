namespace IBC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ibctb001_cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ibctb001_cliente()
        {
            ibctb002_pontos_caixa = new HashSet<ibctb002_pontos_caixa>();
            ibctb003_menu_favorito = new HashSet<ibctb003_menu_favorito>();
        }

        [Key]
        public int co_cliente { get; set; }

        [Required]
        [StringLength(4)]
        public string co_agencia { get; set; }

        [Required]
        [StringLength(10)]
        public string co_conta { get; set; }

        [Required]
        [StringLength(80)]
        public string no_cliente { get; set; }

        [Required]
        [StringLength(11)]
        public string co_cpf { get; set; }

        [Required]
        [StringLength(20)]
        public string tx_login { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ibctb002_pontos_caixa> ibctb002_pontos_caixa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ibctb003_menu_favorito> ibctb003_menu_favorito { get; set; }
    }
}
