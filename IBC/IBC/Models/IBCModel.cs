namespace IBC.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class IBCModel : DbContext
    {
        public IBCModel()
            : base("name=IBCModelConnection")
        {
        }

        public virtual DbSet<ibctb001_cliente> ibctb001_cliente { get; set; }
        public virtual DbSet<ibctb002_pontos_caixa> ibctb002_pontos_caixa { get; set; }
        public virtual DbSet<ibctb003_menu_favorito> ibctb003_menu_favorito { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ibctb001_cliente>()
                .Property(e => e.co_agencia)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ibctb001_cliente>()
                .Property(e => e.co_conta)
                .IsUnicode(false);

            modelBuilder.Entity<ibctb001_cliente>()
                .Property(e => e.no_cliente)
                .IsUnicode(false);

            modelBuilder.Entity<ibctb001_cliente>()
                .Property(e => e.co_cpf)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ibctb001_cliente>()
                 .Property(e => e.tx_login)
                 .IsFixedLength()
                 .IsUnicode(false);

            modelBuilder.Entity<ibctb001_cliente>()
                .HasMany(e => e.ibctb002_pontos_caixa)
                .WithRequired(e => e.ibctb001_cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ibctb001_cliente>()
                .HasMany(e => e.ibctb003_menu_favorito)
                .WithMany(e => e.ibctb001_cliente)
                .Map(m => m.ToTable("ibctb004_cliente_menu_favorito").MapLeftKey("co_cliente").MapRightKey("co_menu_favorito"));
        }
    }
}
