using MediatR;

public class GetIBGEDadosQuery : IRequest<List<GetIBGEDadosResponse>>
{
    public string CPF { get; set; }
}
