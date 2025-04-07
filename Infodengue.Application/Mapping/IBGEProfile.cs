using AutoMapper;
using Infodengue.Domain.Entities;
using Infodengue.Domain.Model;

namespace Infodengue.Application.Mapping
{
    public class IBGEProfile : Profile
    {
        public IBGEProfile()
        {
            CreateMap<IBGEDados, GetIBGEDadosResponse>().ReverseMap();
            CreateMap<IBGEResponse, CreateIBGEResponse>().ReverseMap();
            CreateMap<IBGEDados, IBGEResponse>().ReverseMap();
            CreateMap<Relatorio, CreateIBGEResponse>().ReverseMap();
            CreateMap<Solicitante, SolicitanteResponse>().ReverseMap();
        }
    }
}
