using FluentValidation;
using SZ.PicPaySimplificado.Dominio.Modelos;

namespace SZ.PicPaySimplificado.Dominio.Validator;

public class TransacaoValidator : AbstractValidator<Transacao>
{
    public TransacaoValidator()
    {
        RuleFor(p => p.Valor)
            .NotNull().WithMessage("Valor da transação não informado.");

		RuleFor(p => p.PagadorId)
			.NotNull().WithMessage("Usuário pagador não informado.");

		RuleFor(p => p.PagadorId)
			.NotNull().WithMessage("Usuário recebedor não informado.");
	}
}
