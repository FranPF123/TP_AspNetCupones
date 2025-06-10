using Microsoft.EntityFrameworkCore;
using ProyectoASPNETGRUPOC.Model;

namespace ProyectoASPNETGRUPOC.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
		public DbSet<Usuario> Usuarios { get; set; }
		public DbSet<Roles> Roles { get; set; }
	}
}
