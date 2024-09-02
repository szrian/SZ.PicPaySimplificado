using Microsoft.EntityFrameworkCore;
using SZ.PicPaySimplificado.Dados.Contexto;
using SZ.PicPaySimplificado.Dominio.Interfaces.Repositorios;
using SZ.PicPaySimplificado.Dominio.Modelos;

namespace SZ.PicPaySimplificado.Dados.Repositorios;

public class TransacaoRepositorio : ITransacaoRepositorio
{
	private readonly PicPaySimplificadoContexto _contexto;
	public TransacaoRepositorio(PicPaySimplificadoContexto contexto)
	{
		_contexto = contexto;
		_contexto.Database.GetDbConnection();
	}
	public async Task Adicionar(Transacao transacao)
	{
		await _contexto.Set<Transacao>().AddAsync(transacao);
		await Commit();
	}

	public async Task<IEnumerable<Transacao>> ObterTransacoesPorUsuarioId(Guid usuarioId) =>
		await _contexto.Set<Transacao>().AsNoTracking()
			.Where(p => p.RecebedorId == usuarioId || p.PagadorId == usuarioId)
			.ToListAsync();

	private async Task<int> Commit() =>
		await _contexto.SaveChangesAsync();
}
