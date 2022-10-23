using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IWatchBrandRepository
    {
        Task<bool> AddWatchBrandAsync(WatchBrand watchBrand);
        Task<IList<WatchBrand>> GetAllWatchBrandsAsync();
        Task<WatchBrand> GetWatchBrandbyNameAsync(string watchBrand);
        bool UpdateWatchBrand (WatchBrand watchBrand);
        bool DeleteWatchBrand(WatchBrand watchBrand);
    }
}
