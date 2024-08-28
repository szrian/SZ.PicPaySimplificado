using Microsoft.EntityFrameworkCore;

namespace SZ.PicPaySimplificado.Dados.Contexto
{
	public class PicPaySimplificadoContexto : DbContext
	{
		public PicPaySimplificadoContexto(DbContextOptions<PicPaySimplificadoContexto> options) : base(options)
		{ }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(PicPaySimplificadoContexto).Assembly);
			base.OnModelCreating(modelBuilder);

			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.EnableSensitiveDataLogging();
		}
	}
}
