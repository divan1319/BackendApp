using AutoMapper;
using BackendApp.DTOs;
using BackendApp.Models;

namespace BackendApp.Automappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<CreateBeerDto, Beer>();
            CreateMap<Beer, BeerDto>().ForMember(dto => dto.Id, m=>m.MapFrom(b=> b.BeerId));
       }
    }
}
