using SZ.PicPaySimplificado.Aplicacao.DTOs.Transacao;
using SZ.PicPaySimplificado.Dominio.Modelos;

namespace SZ.PicPaySimplificado.Aplicacao.Conversores;

public interface ITransacaoConversor
{
	Transacao ConverterParaEntidade(TransacaoDto transacaoDto);
	TransacaoDto ConverterParaDto(Transacao transacao);
}

public class TransacaoConversor : ITransacaoConversor
{
	public TransacaoDto ConverterParaDto(Transacao transacao) =>
		new(transacao.Valor,
			transacao.RecebedorId,
			transacao.PagadorId,
			transacao.ValidationResult);

	public Transacao ConverterParaEntidade(TransacaoDto transacaoDto) =>
		new(transacaoDto.Valor,
			transacaoDto.PagadorId,
			transacaoDto.RecebedorId);
}
