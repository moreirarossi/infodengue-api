using AutoMapper;
using Azure;
using Infodengue.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
public class GetIBGEDadosQueryHandler : IRequestHandler<GetIBGEDadosQuery, List<GetIBGEDadosResponse>>
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    public GetIBGEDadosQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetIBGEDadosResponse>> Handle(GetIBGEDadosQuery request, CancellationToken cancellationToken)
    {
        var codigosIBGE = new List<string> { "3550308", "3304557" };

        var dados = await _context.IBGEDados
            .Where(ibge => _context.IBGEDadosRelatorios
                .Where(dr => _context.Relatorios
                    .Where(r => _context.Solicitantes
                        .Where(s => s.CPF == request.CPF && codigosIBGE.Contains(r.CodigoIBGE))
                        .Select(s => s.Id)
                        .Contains(r.SolicitanteId)
                    )
                    .Select(r => r.Id)
                    .Contains(dr.RelatorioId)
                )
                .Select(dr => dr.IBGEDadosId)
                .Contains(ibge.Id)
            )
            .ToListAsync(cancellationToken);

        var response = _mapper.Map<List<GetIBGEDadosResponse>>(dados);

        return response;
    }
}
