using AutoMapper;
using Infodengue.Application.Command;
using Infodengue.Domain.Entities;
using Infodengue.Domain.Enum;
using Infodengue.Domain.Model;
using Infodengue.Infrastructure.Data;
using MediatR;
using System.Globalization;


public class GetRelatorioHandler : IRequestHandler<GetIBGECommand, IEnumerable<RelatorioResponse>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IIBGEService _ibgeService;
    private readonly ApplicationDbContext _context;

    public GetRelatorioHandler(IIBGEService ibgeService, ApplicationDbContext context, IMapper mapper, IMediator mediator)
    {
        _ibgeService = ibgeService;
        _context = context;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<IEnumerable<RelatorioResponse>> Handle(GetIBGECommand request, CancellationToken cancellationToken)
    {

        var solicitante = await ObterSolicitante(request, cancellationToken);

        var relatorioResponse = new List<RelatorioResponse>();

        foreach (var disease in Enum.GetNames(typeof(DiseaseTipo)))
        {
            foreach (var geocode in GeoCode.GeoCodes)
            {

                var IBGEResponse = ObterDadosIBGE(request, disease, geocode);

                var relatorioEntity = new Relatorio
                {
                    SolicitanteId = solicitante.Id,
                    CodigoIBGE = geocode,
                    Municipio = GeoCode.ObterMunicipio(geocode),
                    Arbovirose = disease,
                    SemanaInicio = request.SemanaInicio,
                    SemanaTermino = request.SemanaTermino,
                    DataSolicitacao = DateTime.Now,
                    TotalCasos = IBGEResponse.Result.Sum(x => x.Casos)
                };

                _context.Relatorios.Add(relatorioEntity);
                await _context.SaveChangesAsync(cancellationToken);
                relatorioResponse.Add(_mapper.Map<RelatorioResponse>(relatorioEntity));
            }
        }

        return relatorioResponse;
    }

    private int GetWeekOfYear(DateTime date, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
    {
        var cultureInfo = new CultureInfo("pt-BR");
        var calendar = cultureInfo.Calendar;
        return calendar.GetWeekOfYear(date, rule, firstDayOfWeek);
    }

    private async Task<IEnumerable<IBGEResponse>> ObterDadosIBGE(GetIBGECommand request, string disease, string geocode)
    {
        int semanaInicio = GetWeekOfYear(request.SemanaInicio, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        int semanaTermino = GetWeekOfYear(request.SemanaTermino, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        var ibgeResponse = await _ibgeService.ConsultarRelatorioAsync(
            geocode: geocode,
            disease: disease,
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

        return ibgeResponse;
    }

    private async Task<Solicitante> ObterSolicitante(GetIBGECommand request, CancellationToken cancellationToken)
    {
        var solicitante = await _mediator.Send(new CreateSolicitanteCommand() { CPF = request.CPF, Nome = request.Nome }, cancellationToken);

        if (!solicitante.Success)
        {
            throw new Exception("Erro obtendo solicitante.");
        }

        return _mapper.Map<Solicitante>(solicitante.Data);
    }

}
