using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IWatchModelRepository
    {
        Task<bool> AddWatchModelAsync(WatchModel watchModel);
        bool UpdateWatchModel(WatchModel watchModel);
        bool RemoveWatchModel(WatchModel watchModel);
        Task<IList<WatchModel>> GetAllWatchModelsAsync();
        Task<WatchModel> FindWatchModelByIdAsync(int id);
        Task<WatchModel> FindWatchModelByNameAsync(string name);
        Task<IList<WatchModel>> FindWatchModelsByWatchBrand(string brandName);
    }
}
