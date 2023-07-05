using AutoMapper;
using BenevArts.Data.Models;
using BenevArts.Web.ViewModels.Home;

namespace BenevArts.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Asset, AssetViewModel>();
            CreateMap<AddAssetViewModel, Asset>();
        }
    }
}
