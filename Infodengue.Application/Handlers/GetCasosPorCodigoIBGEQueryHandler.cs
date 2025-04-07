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
        var response = await (
            from dr in _context.IBGEDadosRelatorios
            join r in _context.Relatorios on dr.RelatorioId equals r.Id
            join s in _context.Solicitantes on r.SolicitanteId equals s.Id
            join ibge in _context.IBGEDados on dr.IBGEDadosId equals ibge.Id
            where s.CPF == request.CPF && r.CodigoIBGE == request.CodigoIBGE
            select new { r.CodigoIBGE, ibge.Id, ibge.Casos }
        )
        .Distinct()
        .GroupBy(x => x.CodigoIBGE)
        .Select(g => new CasosPorCodigoIBGEResponse
        {
            CodigoIBGE = g.Key!,
            TotalCasos = g.Sum(x => x.Casos ?? 0)
        })
        .ToListAsync();

        return Result<List<CasosPorCodigoIBGEResponse>>.Ok(response);
    }
}
