using AutoMapper;
using Cars.Api.Entity;
using Cars.Api.Models;

namespace Cars.Api.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CarModel, CarEntity>()
                .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Brand));
            CreateMap<CarCreateModel, CarEntity>()
                .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Brand));
        }
    }
}
