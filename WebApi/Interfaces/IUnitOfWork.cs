using WebApi.Data;
using WebApi.Repositories;

namespace WebApi.Interfaces
{
    public interface IUnitOfWork
    {
        IWatchModelRepository WatchModelRepository { get; }
        IWatchBrandRepository WatchBrandRepository { get; }
        DataContext Context { get; }
        Task<bool> Complete();
        bool HasChanged();
    }
}
