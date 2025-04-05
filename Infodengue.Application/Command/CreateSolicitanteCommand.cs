using Infodengue.Domain.Entities;
using MediatR;

public class CreateSolicitanteCommand : IRequest<Result<Solicitante>>
{
    public string Nome { get; set; }
    public string CPF { get; set; }
}
