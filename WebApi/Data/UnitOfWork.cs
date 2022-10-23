using WebApi.Interfaces;
using WebApi.Repositories;

namespace WebApi.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IWatchModelRepository WatchModelRepository => new WatchModelRepository(_context);

        public IWatchBrandRepository WatchBrandRepository => new WatchBrandRepository(_context);

        public DataContext Context => _context;

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanged()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}
