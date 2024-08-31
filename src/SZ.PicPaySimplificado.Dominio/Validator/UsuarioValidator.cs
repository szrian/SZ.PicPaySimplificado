using FluentValidation;
using SZ.PicPaySimplificado.Dominio.Modelos;

namespace SZ.PicPaySimplificado.Dominio.Validator
{
	public class UsuarioValidator : AbstractValidator<Usuario>
	{
        public UsuarioValidator()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O nome não pode ser vazio.");

			RuleFor(p => p.Email)
                .NotEmpty().WithMessage("O e-mail não pode ser vazio.")
                .EmailAddress().WithMessage("O e-mail informado não é válido.");

            RuleFor(p => p.Documento)
                .NotEmpty().WithMessage("O documento não pode ser vazio.")
                .MaximumLength(14).WithMessage("O documento informado não é válido.");
		}
    }
}
