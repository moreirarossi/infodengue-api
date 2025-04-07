using FluentValidation;
using Infodengue.Application.Command;

namespace Infodengue.Application.Validations
{
    public class ConsultarIBGEValidator : AbstractValidator<CreateIBGECommand>
    {
        public ConsultarIBGEValidator()
        {
            RuleFor(x => x.CPF)
                .NotEmpty()
                .WithMessage(x => $"Informe seu CPF.")
                .Length(11)
                .WithMessage(x => $"CPF ifnormado é inválido.");

            RuleFor(x => x.Nome)
                .Must(nome => !string.IsNullOrEmpty(nome))
                .WithMessage(x => $"Informe seu nome.");
        }
    }
}