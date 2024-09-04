using SZ.PicPaySimplificado.Aplicacao.DTOs.Transacao;

namespace SZ.PicPaySimplificado.Aplicacao.Interfaces;

public interface ITransacaoAppService
{
	Task Adicionar(TransacaoDto transacaoDto);
}
