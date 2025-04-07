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
        var codigosIBGE = new List<string> { "3550308", "3304557" };

        var casosPorMunicipio = await (
            from dr in _context.IBGEDadosRelatorios
            join r in _context.Relatorios on dr.RelatorioId equals r.Id
            join s in _context.Solicitantes on r.SolicitanteId equals s.Id
            join ibge in _context.IBGEDados on dr.IBGEDadosId equals ibge.Id
            where s.CPF == request.CPF && codigosIBGE.Contains(r.CodigoIBGE!)
            select new { r.Municipio, ibge.Id, ibge.Casos }
        )
        .Distinct()
        .GroupBy(x => x.Municipio)
        .Select(g => new CasosPorMunicipioResponse
        {
            Municipio = g.Key!,
            TotalCasos = g.Sum(x => x.Casos ?? 0)
        })
        .ToListAsync();

        return Result<List<CasosPorMunicipioResponse>>.Ok(casosPorMunicipio);
    }
}
