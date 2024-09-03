using SZ.PicPaySimplificado.Dominio.Modelos;

namespace SZ.PicPaySimplificado.Dominio.Interfaces.Servicos;

public interface ITransacaoServico
{
	Task Adicionar(Transacao transacao);
}
