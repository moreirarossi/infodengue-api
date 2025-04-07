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
        var casosPorArbovirose = await (
            from dr in _context.IBGEDadosRelatorios
            join r in _context.Relatorios on dr.RelatorioId equals r.Id
            join s in _context.Solicitantes on r.SolicitanteId equals s.Id
            join ibge in _context.IBGEDados on dr.IBGEDadosId equals ibge.Id
            where s.CPF == request.CPF
            select new { r.Arbovirose, r.Municipio, ibge.Id, ibge.Casos }
        )
        .Distinct()
        .GroupBy(x => new { x.Arbovirose, x.Municipio })
        .Select(g => new CasosPorArboviroseResponse
        {
            Arbovirose = g.Key.Arbovirose!,
            Municipio = g.Key.Municipio!,
            TotalCasos = g.Sum(x => x.Casos ?? 0)
        })
        .ToListAsync();

        return Result<List<CasosPorArboviroseResponse>>.Ok(casosPorArbovirose);
    }
}
