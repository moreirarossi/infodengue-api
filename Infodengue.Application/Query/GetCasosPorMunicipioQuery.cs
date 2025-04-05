using MediatR;

public class GetCasosPorMunicipioQuery : IRequest<Result<List<CasosPorMunicipioResponse>>>
{
    public string? Municipio { get; set; }
}
