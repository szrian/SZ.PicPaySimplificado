using FluentValidation;
using SZ.PicPaySimplificado.Dominio.Modelos;

namespace SZ.PicPaySimplificado.Dominio.Validator;

public class TransacaoValidator : AbstractValidator<Transacao>
{
    public TransacaoValidator()
    {
        RuleFor(p => p.Valor)
            .NotNull().WithMessage("Valor da transação não informado.")
			.GreaterThan(0).WithMessage("Valor informado deve ser maior que 0.");

		RuleFor(p => p.PagadorId)
			.NotEmpty().WithMessage("Usuário pagador não informado.");

		RuleFor(p => p.RecebedorId)
			.NotEmpty().WithMessage("Usuário recebedor não informado.");
	}
}
