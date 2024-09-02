using SZ.PicPaySimplificado.Dominio.Modelos;

namespace SZ.PicPaySimplificado.Dominio.Interfaces.Repositorios;

public interface ITransacaoRepositorio
{
	Task Adicionar(Transacao transacao);
	Task<IEnumerable<Transacao>> ObterTransacoesPorUsuarioId(Guid usuarioId);
}
