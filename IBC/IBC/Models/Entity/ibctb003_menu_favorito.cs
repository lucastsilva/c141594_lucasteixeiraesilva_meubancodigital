namespace IBC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ibctb003_menu_favorito
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ibctb003_menu_favorito()
        {
            ibctb001_cliente = new HashSet<ibctb001_cliente>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int co_menu_favorito { get; set; }

        [Required]
        [MaxLength(100)]
        public string no_menu_favorito { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ibctb001_cliente> ibctb001_cliente { get; set; }
    }
}
