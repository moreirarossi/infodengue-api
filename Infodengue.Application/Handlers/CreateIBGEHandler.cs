using AutoMapper;
using Azure.Core;
using Infodengue.Application.Command;
using Infodengue.Domain.Entities;
using Infodengue.Domain.Enum;
using Infodengue.Domain.Model;
using Infodengue.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Threading;


public class CreateIBGEHandler : IRequestHandler<CreateIBGECommand, Result<CreateIBGEResponse>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IIBGEService _ibgeService;
    private readonly ApplicationDbContext _context;

    public CreateIBGEHandler(IIBGEService ibgeService, ApplicationDbContext context, IMapper mapper, IMediator mediator)
    {
        _ibgeService = ibgeService;
        _context = context;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<Result<CreateIBGEResponse>> Handle(CreateIBGECommand request, CancellationToken cancellationToken)
    {

        var solicitante = await ObterSolicitante(request, cancellationToken);

        var responseIBGE = ObterDadosIBGE(request);

        var relatorioEntity = new Relatorio
        {
            SolicitanteId = solicitante.Id,
            CodigoIBGE = request.CodigoIBGE,
            Municipio = GeoCode.ObterMunicipio(request.CodigoIBGE),
            Arbovirose = request.Arbovirose.ToString(),
            SemanaInicio = request.SemanaInicio,
            SemanaTermino = request.SemanaTermino,
            DataSolicitacao = DateTime.Now,
            TotalCasos = responseIBGE.Result.Sum(x => x.Casos)
        };

        _context.Relatorios.Add(relatorioEntity);
        await _context.SaveChangesAsync(cancellationToken);

        foreach ( var response in responseIBGE.Result)
        {
            var dados = await GravaIBGEDados(response, cancellationToken);

            await VinculaDadosComRelatorio(relatorioEntity.Id, dados.Id, cancellationToken);

        }

        return Result<CreateIBGEResponse>.Ok(_mapper.Map<CreateIBGEResponse>(relatorioEntity));
    }

    private int GetWeekOfYear(DateTime date, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
    {
        var cultureInfo = new CultureInfo("pt-BR");
        var calendar = cultureInfo.Calendar;
        return calendar.GetWeekOfYear(date, rule, firstDayOfWeek);
    }

    private async Task<IEnumerable<IBGEResponse>> ObterDadosIBGE(CreateIBGECommand request)
    {
        int semanaInicio = GetWeekOfYear(request.SemanaInicio, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        if (request.SemanaInicio.Month == 1 && semanaInicio > 8)
            semanaInicio = 1;
        int semanaTermino = GetWeekOfYear(request.SemanaTermino, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        var ibgeResponse = await _ibgeService.ConsultarRelatorioAsync(
            geocode: request.CodigoIBGE,
            disease: request.Arbovirose.ToString(),
            format: "json",
            ew_start: semanaInicio,
            ew_end: semanaTermino,
            ey_start: request.SemanaInicio.Year,
            ey_end: request.SemanaTermino.Year
            );

        if (ibgeResponse is null)
        {
            throw new Exception("Erro ao consultar dados do InfoDengue.");
        }

        if (ibgeResponse.Count() == 0)
        {
            throw new Exception("Não há dados para consulta.");
        }

        return ibgeResponse;
    }

    private async Task<Solicitante> ObterSolicitante(CreateIBGECommand request, CancellationToken cancellationToken)
    {
        var solicitante = await _mediator.Send(new CreateSolicitanteCommand() { CPF = request.CPF, Nome = request.Nome }, cancellationToken);

        if (!solicitante.Success)
        {
            throw new Exception("Erro obtendo solicitante.");
        }

        return _mapper.Map<Solicitante>(solicitante.Data);
    }

    private async Task<IBGEDados> GravaIBGEDados(IBGEResponse response, CancellationToken cancellationToken)
    {
        {
            var dados = await _context.IBGEDados
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == response.Id, cancellationToken);

            if (dados!= null)
            {
                return dados;
            }

            dados = _mapper.Map(response, dados);

            _context.IBGEDados.Add(dados);
            await _context.SaveChangesAsync(cancellationToken);

            return dados;
        }
    }

    private async Task VinculaDadosComRelatorio(int relatorioId, long dadosId, CancellationToken cancellationToken)
    {
        var entity = new IBGEDadosRelatorio
        {
            RelatorioId = relatorioId,
            IBGEDadosId = dadosId
        };
        _context.IBGEDadosRelatorios.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

}
