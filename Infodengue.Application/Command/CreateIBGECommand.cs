using Infodengue.Domain.Enum;
using Infodengue.Domain.Model;
using MediatR;

namespace Infodengue.Application.Command
{
    public class CreateIBGECommand : IRequest<Result<CreateIBGEResponse>>
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public ArboviroseTipo Arbovirose { get; set; }
        public DateTime SemanaInicio { get; set; }
        public DateTime SemanaTermino { get; set; }
        public string CodigoIBGE { get; set; }
    }
}
