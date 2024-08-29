using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SZ.PicPaySimplificado.Dominio.Modelos;

namespace SZ.PicPaySimplificado.Dados.EntidadesConfig;

internal class TransacaoConfig : IEntityTypeConfiguration<Transacao>
{
	private const string NomeTabela = "Transacoes";
	public void Configure(EntityTypeBuilder<Transacao> builder)
	{
		builder.HasKey(p => p.Id);

		builder.Property(p => p.Valor)
			.IsRequired();

		builder.HasOne(p => p.Pagador)
			.WithMany(q => q.Transacoes)
			.HasForeignKey(r => r.PagadorId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(p => p.Recebedor)
			.WithMany()
			.HasForeignKey(r => r.RecebedorId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.ToTable(NomeTabela);
	}
}
