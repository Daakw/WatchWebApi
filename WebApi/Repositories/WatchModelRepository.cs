using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class WatchModelRepository : IWatchModelRepository
    {
        private readonly DataContext _context;

        public WatchModelRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<bool> AddWatchModelAsync(WatchModel watchModel)
        {
            try
            {
                await _context.WatchModels.AddAsync(watchModel);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<WatchModel> FindWatchModelByIdAsync(int id)
        {
            return await _context.WatchModels.Include(c => c.WatchBrand).SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IList<WatchModel>> FindWatchModelByNameAsync(string name)
        {
            return await _context.WatchModels.Include(c => c.WatchBrand).Where(c => c.ModelName.Trim().ToLower() == name.Trim().ToLower()).ToListAsync();
        }

        public async Task<IList<WatchModel>> FindWatchModelsByWatchBrand(string brandName)
        {
            return await _context.WatchModels.Include(c => c.WatchBrand).Where(c => c.WatchBrand.BrandName.Trim().ToLower() == brandName.Trim().ToLower()).ToListAsync();
        }

        public async Task<IList<WatchModel>> GetAllWatchModelsAsync()
        {
                return await _context.WatchModels.Include(c => c.WatchBrand).ToListAsync();
        }

        public bool RemoveWatchModel(WatchModel watchModel)
        {
                try
                {
                    _context.WatchModels.Remove(watchModel);
                    return true;
                }
                catch
                {
                    return false;
                }
        }

        public bool UpdateWatchModel(WatchModel watchModel)
        {
                try
                {
                    _context.WatchModels.Update(watchModel);
                    return true;
                }
                catch
                {
                    return false;
                }
        }
    }
}
