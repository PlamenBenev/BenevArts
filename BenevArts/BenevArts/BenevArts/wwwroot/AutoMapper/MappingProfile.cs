using AutoMapper;
using BenevArts.Data.Models;
using BenevArts.Web.ViewModels.Home;

namespace BenevArts.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Asset, AssetViewModel>()
                 .ForMember(dest => dest.UploadDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                 .ForMember(dest => dest.Seller,
                    opt => opt.MapFrom(src => src.Seller.Name))
                 .ForMember(dest => dest.Category,
                    opt => opt.MapFrom(src => src.Category.Name));


            CreateMap<AddAssetViewModel, Asset>()
                .ForMember(dest => dest.Images,
                    opt => opt.MapFrom(src => src.Images.Select(image => new AssetImage { ImageName = image.FileName })))
                .ForMember(dest => dest.ZipFileName,
                    opt => opt.MapFrom(src => src.ZipFileName.FileName));

        }
    }
}
