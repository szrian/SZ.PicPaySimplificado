using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SZ.PicPaySimplificado.Dominio.Modelos;

namespace SZ.PicPaySimplificado.Dados.EntidadesConfig;

internal class UsuarioConfig : IEntityTypeConfiguration<Usuario>
{
	private const string NomeTabela = "Usuarios";
	public void Configure(EntityTypeBuilder<Usuario> builder)
	{
		builder.HasKey(p => p.Id);

		builder.HasIndex(p => p.Documento).IsUnique();
		builder.HasIndex(p => p.Email).IsUnique();

		builder.Property(p => p.Nome)
			.IsRequired()
			.HasMaxLength(200);

		builder.Property(p => p.Documento)
			.IsRequired()
			.HasMaxLength(14);

		builder.Property(p => p.Email)
			.IsRequired()
			.HasMaxLength(120);

		builder.Property(p => p.TipoUsuario)
			.IsRequired();

		builder.Property(p => p.Saldo)
			.IsRequired();

		builder.ToTable(NomeTabela);
	}
}
