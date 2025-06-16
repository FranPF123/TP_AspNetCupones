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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<CuponesDetalle>().HasKey(c => new { c.Id_Cupon, c.Id_Articulo });
		}
	}
}
