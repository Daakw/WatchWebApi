using AutoMapper;
using WebApi.Models;
using WebApi.ViewModels.Brand;

namespace WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<WatchBrand, ViewModels.Brand.ViewModel>();
            CreateMap<ViewModels.Brand.PostViewModel, WatchBrand>();

            CreateMap<ViewModels.Model.PostViewModel, WatchModel>().ForMember(dest => dest.WatchBrand, opt => opt.MapFrom<AddWatchBrandResolver>());

            CreateMap<WatchModel, ViewModels.Model.ViewModel>().ForMember(dest => dest.WatchBrand, opt => opt.MapFrom(src => src.WatchBrand.BrandName));
        }
    }
}
