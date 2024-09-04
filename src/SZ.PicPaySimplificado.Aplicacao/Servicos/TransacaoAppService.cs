using SZ.PicPaySimplificado.Aplicacao.Conversores;
using SZ.PicPaySimplificado.Aplicacao.DTOs.Transacao;
using SZ.PicPaySimplificado.Aplicacao.Interfaces;
using SZ.PicPaySimplificado.Dominio.Interfaces.Servicos;

namespace SZ.PicPaySimplificado.Aplicacao.Servicos;

public class TransacaoAppService : ITransacaoAppService
{
	private readonly ITransacaoServico _transacaoServico;
	private readonly ITransacaoConversor _conversor;

	public TransacaoAppService(ITransacaoServico transacaoServico, 
		ITransacaoConversor conversor)
	{
		_transacaoServico = transacaoServico;
		_conversor = conversor;
	}

	public async Task Adicionar(TransacaoDto transacaoDto)
	{
		var transacao = _conversor.ConverterParaEntidade(transacaoDto);
		await _transacaoServico.Adicionar(transacao);
	}
}
