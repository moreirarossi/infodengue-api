using Infodengue.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetCasosPorCodigoIBGEQueryHandler : IRequestHandler<GetCasosPorCodigoIBGEQuery, Result<List<CasosPorCodigoIBGEResponse>>>
{
    private readonly ApplicationDbContext _context;

    public GetCasosPorCodigoIBGEQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<CasosPorCodigoIBGEResponse>>> Handle(GetCasosPorCodigoIBGEQuery request, CancellationToken cancellationToken)
    {
        var resultado = await _context.Relatorios
            .AsNoTracking()
            .Where(r => (!String.IsNullOrEmpty(request.CodigoIBGE) && r.CodigoIBGE == request.CodigoIBGE)
                       || (String.IsNullOrEmpty(request.CodigoIBGE) && r.CodigoIBGE != null)
                       )
            .GroupBy(r => r.CodigoIBGE)
            .Select(g => new CasosPorCodigoIBGEResponse
            {
                CodigoIBGE = g.Key!,
                TotalCasos = g.Sum(x => x.TotalCasos ?? 0)
            })
            .ToListAsync(cancellationToken);

        if (resultado == null || resultado.Count == 0)
            return Result<List<CasosPorCodigoIBGEResponse>>.Fail("Nenhum dado encontrado.");

        return Result<List<CasosPorCodigoIBGEResponse>>.Ok(resultado);
    }
}
