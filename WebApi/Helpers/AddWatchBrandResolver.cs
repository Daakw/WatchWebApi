using AutoMapper;
using WebApi.Data;
using WebApi.Models;


namespace WebApi.Helpers
{
    public class AddWatchBrandResolver : IValueResolver<ViewModels.Model.PostViewModel, WatchModel, WatchBrand>
    {
        public WatchBrand Resolve(ViewModels.Model.PostViewModel source, WatchModel destination, WatchBrand destMember, ResolutionContext context)
        {
            var repo = context.Items["repo"] as DataContext;
            var result = repo.WatchBrands.FirstOrDefault(x => x.BrandName.ToLower().Trim() == source.BrandName.ToLower().Trim());

            if (result == null) throw new Exception($"Could not find any watchbrand with the name \"{source.BrandName}\"");
            return result;
        }
    }
}
