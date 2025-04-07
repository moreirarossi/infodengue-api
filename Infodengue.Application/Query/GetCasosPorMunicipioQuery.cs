using MediatR;

public class GetCasosPorMunicipioQuery : IRequest<Result<List<CasosPorMunicipioResponse>>>
{
    public string CPF { get; set; }
}
