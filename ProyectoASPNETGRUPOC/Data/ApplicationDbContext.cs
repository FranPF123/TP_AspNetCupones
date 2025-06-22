using Microsoft.EntityFrameworkCore;
using ProyectoASPNETGRUPOC.Model;

namespace ProyectoASPNETGRUPOC.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
		public DbSet<Usuario> Usuarios { get; set; }
		public DbSet<Roles> Roles { get; set; }
		public DbSet<Cupones> Cupones {  get; set; }
		public DbSet<Articulos> Articulos { get; set; }
		public DbSet<TipoCupon> Tipo_Cupon {  get; set; }
		public DbSet<CuponesDetalle> Cupones_Detalle { get; set; }
        public DbSet<CuponesClientes> Cupones_Clientes { get; set; }
		public DbSet<CuponesHistorial> Cupones_Historial { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<CuponesDetalle>().HasKey(cd => new { cd.Id_Cupon, cd.Id_Articulo });

            modelBuilder.Entity<CuponesClientes>().HasKey(cc => new { cc.Id_Cupon, cc.Id_Usuario });

            modelBuilder.Entity<CuponesHistorial>().HasKey(ch => new { ch.NroCupon, ch.Id_Usuario });
        }
	}
}
