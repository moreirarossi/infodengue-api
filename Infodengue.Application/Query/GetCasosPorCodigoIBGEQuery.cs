using MediatR;

public class GetCasosPorCodigoIBGEQuery : IRequest<Result<List<CasosPorCodigoIBGEResponse>>>
{
    public string CPF { get; set; }
    public string CodigoIBGE { get; set; }
}
