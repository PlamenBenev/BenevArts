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
                 .ForMember(dest => dest.Images, opt => opt.Ignore())
                 .ForMember(dest => dest.ZipFileName,
                    opt => opt.MapFrom(src => src.ZipFileName.FileName));

            CreateMap<EditAssetViewModel, Asset>()
                 .ForMember(dest => dest.Images, opt => opt.Ignore());

            CreateMap<Asset, EditAssetViewModel>();

            CreateMap<EditAssetViewModel, CategoryViewModel>();

            CreateMap<Asset, CategoryViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Category.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Comment, CommentViewModel>();

            CreateMap<Like, LikeViewModel>();
        }
    }
}
