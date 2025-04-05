using AutoMapper;
using Infodengue.Domain.Entities;
using Infodengue.Domain.Model;

namespace Infodengue.Application.Mapping
{
    public class IBGEProfile : Profile
    {
        public IBGEProfile()
        {
            CreateMap<IBGEResponse, RelatorioResponse>().ReverseMap();
            CreateMap<Relatorio, RelatorioResponse>().ReverseMap();
            CreateMap<Solicitante, SolicitanteResponse>().ReverseMap();
        }
    }
}
