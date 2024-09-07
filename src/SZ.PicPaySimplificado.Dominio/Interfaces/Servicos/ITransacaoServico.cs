using SZ.PicPaySimplificado.Dominio.Modelos;

namespace SZ.PicPaySimplificado.Dominio.Interfaces.Servicos;

public interface ITransacaoServico
{
	Task<Transacao> Adicionar(Transacao transacao);
}
