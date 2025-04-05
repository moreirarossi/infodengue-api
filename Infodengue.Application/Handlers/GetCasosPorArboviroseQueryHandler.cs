using Infodengue.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetCasosPorArboviroseQueryHandler : IRequestHandler<GetTotalCasosPorArboviroseQuery, Result<List<CasosPorArboviroseResponse>>>
{
    private readonly ApplicationDbContext _context;

    public GetCasosPorArboviroseQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<CasosPorArboviroseResponse>>> Handle(GetTotalCasosPorArboviroseQuery request, CancellationToken cancellationToken)
    {
        var resultado = await _context.Relatorios
            .AsNoTracking()
            .Where(r => (!String.IsNullOrEmpty(request.Arbovirose) && r.Arbovirose == request.Arbovirose)
                        || (String.IsNullOrEmpty(request.Arbovirose) && r.Arbovirose != null)
                        )
            .GroupBy(r => r.Arbovirose)
            .Select(g => new CasosPorArboviroseResponse
            {
                Arbovirose = g.Key!,
                TotalCasos = g.Sum(x => x.TotalCasos ?? 0)
            })
            .ToListAsync(cancellationToken);

        if (resultado == null || resultado.Count == 0)
            return Result<List<CasosPorArboviroseResponse>>.Fail("Nenhum dado encontrado.");

        return Result<List<CasosPorArboviroseResponse>>.Ok(resultado);
    }
}
