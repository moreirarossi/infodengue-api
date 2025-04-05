using Infodengue.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetCasosPorMunicipioQueryHandler : IRequestHandler<GetCasosPorMunicipioQuery, Result<List<CasosPorMunicipioResponse>>>
{
    private readonly ApplicationDbContext _context;

    public GetCasosPorMunicipioQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<CasosPorMunicipioResponse>>> Handle(GetCasosPorMunicipioQuery request, CancellationToken cancellationToken)
    {
        var resultado = await _context.Relatorios
            .AsNoTracking()
            .Where(r => (!String.IsNullOrEmpty(request.Municipio) && r.Municipio == request.Municipio)
                        || (String.IsNullOrEmpty(request.Municipio) && r.Municipio != null)
                        )
            .GroupBy(r => r.Municipio)
            .Select(g => new CasosPorMunicipioResponse
            {
                Municipio = g.Key!,
                TotalCasos = g.Sum(x => x.TotalCasos ?? 0)
            })
            .ToListAsync(cancellationToken);

        if (resultado == null || resultado.Count == 0)
            return Result<List<CasosPorMunicipioResponse>>.Fail("Nenhum dado encontrado.");

        return Result<List<CasosPorMunicipioResponse>>.Ok(resultado);
    }
}
