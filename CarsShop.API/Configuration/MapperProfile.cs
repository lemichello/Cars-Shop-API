using System.Linq;
using AutoMapper;
using CarsShop.DAL.Entities;
using CarsShop.DTO;

namespace CarsShop.API.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Car, PresentationCarDto>()
                .ForMember(dest => dest.Color,
                    opt => opt.MapFrom(src => src.Color.Name))
                .ForMember(dest => dest.Model,
                    opt => opt.MapFrom(src => src.Model.Name))
                .ForMember(dest => dest.Vendor,
                    opt => opt.MapFrom(src => src.Model.Vendor.Name))
                .ForMember(dest => dest.EngineVolume,
                    opt => opt.MapFrom(src => src.EngineVolume.Volume))
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => src.PriceHistories.Last().Price));
        }
    }
}