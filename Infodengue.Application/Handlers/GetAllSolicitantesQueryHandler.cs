using AutoMapper;
using Infodengue.Domain.Model;
using Infodengue.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetAllSolicitantesQueryHandler : IRequestHandler<GetAllSolicitantesQuery, Result<List<SolicitanteResponse>>>
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    public GetAllSolicitantesQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<SolicitanteResponse>>> Handle(GetAllSolicitantesQuery request, CancellationToken cancellationToken)
    {
        var solicitantes = await _context.Solicitantes
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        if (solicitantes == null || solicitantes.Count == 0)
        {
            return Result<List<SolicitanteResponse>>.Fail("Nenhum solicitante encontrado.");
        }

        var response = _mapper.Map<List<SolicitanteResponse>>(solicitantes);

        return Result<List<SolicitanteResponse>>.Ok(response);
    }
}
