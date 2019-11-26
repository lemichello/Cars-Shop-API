using System;
using System.Linq;
using AutoMapper;
using CarsShop.DAL.Entities;
using CarsShop.DTO.CarsDto;
using CarsShop.DTO.ColorsDto;
using CarsShop.DTO.EngineVolumesDto;
using CarsShop.DTO.ModelsDto;
using CarsShop.DTO.PriceHistoriesDto;
using CarsShop.DTO.VendorsDto;

namespace CarsShop.API.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PriceHistory, PriceHistoryDto>();

            CreateMap<CarDto, PriceHistory>()
                .ForMember(dst => dst.CarId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Date, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dst => dst.Id, opt => opt.Ignore());

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

            CreateMap<Car, CarDto>()
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.PriceHistories.Last().Price));
            CreateMap<CarDto, Car>();
            CreateMap<Car, EditCarDto>()
                .ForMember(dst => dst.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.PriceHistories.Last().Price));
            CreateMap<Car, DetailedCarDto>()
                .ForMember(dest => dest.Color,
                    opt => opt.MapFrom(src => src.Color.Name))
                .ForMember(dest => dest.Model,
                    opt => opt.MapFrom(src => src.Model.Name))
                .ForMember(dest => dest.Vendor,
                    opt => opt.MapFrom(src => src.Model.Vendor.Name))
                .ForMember(dest => dest.EngineVolume,
                    opt => opt.MapFrom(src => src.EngineVolume.Volume))
                .ForMember(dest => dest.PricesHistory,
                    opt => opt.MapFrom(src => src.PriceHistories.Select(i => i)));

            CreateMap<Color, ColorDto>();
            CreateMap<ColorDto, Color>();

            CreateMap<Vendor, VendorDto>();
            CreateMap<VendorDto, Vendor>();
            CreateMap<Vendor, DetailedVendorDto>();

            CreateMap<EngineVolume, EngineVolumeDto>();
            CreateMap<EngineVolumeDto, EngineVolume>();

            CreateMap<Model, ModelDto>();
            CreateMap<ModelDto, Model>();
        }
    }
}