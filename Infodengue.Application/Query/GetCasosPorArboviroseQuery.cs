using MediatR;

public class GetTotalCasosPorArboviroseQuery : IRequest<Result<List<CasosPorArboviroseResponse>>>
{
    public string? Arbovirose { get; set; }
}
