using System;
using System.Linq;
using AutoMapper;
using CarsShop.Data.Entities;
using CarsShop.DTO.CarsDto;
using CarsShop.DTO.ColorsDto;
using CarsShop.DTO.EngineVolumesDto;
using CarsShop.DTO.ModelsDto;
using CarsShop.DTO.PriceHistoriesDto;
using CarsShop.DTO.VendorsDto;

namespace CarsShop.API.Configuration.MapperProfiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PriceHistory, PriceHistoryDto>();

            CreateMap<EditCarDto, PriceHistory>()
                .ForMember(dst => dst.CarId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Date, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dst => dst.Id, opt => opt.Ignore());

            CreateMap<Car, CarDto>()
                .ForMember(dest => dest.Color,
                    opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.Model,
                    opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.Vendor,
                    opt => opt.MapFrom(src => src.Model.Vendor))
                .ForMember(dest => dest.EngineVolume,
                    opt => opt.MapFrom(src => src.EngineVolume))
                .ForMember(dest => dest.PricesHistory,
                    opt => opt.MapFrom(src => src.PriceHistories.Select(i => i)));

            CreateMap<Car, EditCarDto>()
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.PriceHistories.Last().Price));
            CreateMap<EditCarDto, Car>();

            CreateMap<Color, ColorDto>();
            CreateMap<ColorDto, Color>();

            CreateMap<Vendor, VendorDto>();
            CreateMap<VendorDto, Vendor>();

            CreateMap<EngineVolume, EngineVolumeDto>();
            CreateMap<EngineVolumeDto, EngineVolume>();

            CreateMap<Model, ModelDto>();
            CreateMap<ModelDto, Model>();
        }
    }
}