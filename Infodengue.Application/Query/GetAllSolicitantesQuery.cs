using Infodengue.Domain.Model;
using MediatR;

public class GetAllSolicitantesQuery : IRequest<Result<List<SolicitanteResponse>>>
{
}
