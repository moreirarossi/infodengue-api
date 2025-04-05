using MediatR;

public class GetCasosPorCodigoIBGEQuery : IRequest<Result<List<CasosPorCodigoIBGEResponse>>>
{
    public string? CodigoIBGE { get; set; }
}
