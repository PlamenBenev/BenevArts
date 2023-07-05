using AutoMapper;
using BenevArts.Data.Models;

namespace BenevArts.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Asset, AssetViewModel>();
        }
    }
}
