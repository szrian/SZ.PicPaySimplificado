using FluentValidation.Results;

namespace SZ.PicPaySimplificado.Aplicacao.DTOs.Transacao;

public class TransacaoDto
{
	public TransacaoDto(float valor,
		Guid recebedorId,
		Guid pagadorId,
		ValidationResult validationResult)
	{
		Valor = valor;
		RecebedorId = recebedorId;
		PagadorId = pagadorId;
		ValidationResult = validationResult;
	}
	public float Valor { get; set; }
	public Guid RecebedorId { get; set; }
	public Guid PagadorId { get; set; }

	public ValidationResult ValidationResult { get; set; }
}
