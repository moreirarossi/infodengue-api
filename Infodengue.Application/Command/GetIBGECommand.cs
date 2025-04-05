using Infodengue.Domain.Model;
using MediatR;

namespace Infodengue.Application.Command
{
    public class GetIBGECommand : IRequest<IEnumerable<RelatorioResponse>>
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime SemanaInicio { get; set; }
        public DateTime SemanaTermino { get; set; }
    }
}
