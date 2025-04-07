using MediatR;

public class GetTotalCasosPorArboviroseQuery : IRequest<Result<List<CasosPorArboviroseResponse>>>
{
    public string CPF { get; set; }
}
