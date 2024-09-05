namespace SZ.PicPaySimplificado.Clients.AutorizadorTransacoes.Interfaces;

public interface IAutorizacaoTransacaoServico
{
	Task<bool> AutorizarTransacao();
}
