using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class WatchBrandRepository : IWatchBrandRepository
    {
        private readonly DataContext _context;

        public WatchBrandRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddWatchBrandAsync(WatchBrand watchBrand)
        {
            try
            {
                await _context.WatchBrands.AddAsync(watchBrand);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IList<WatchBrand>> GetAllWatchBrandsAsync()
        {
            return await _context.WatchBrands.ToListAsync();
        }

        public async Task<WatchBrand> GetWatchBrandbyNameAsync(string watchBrand)
        {
            var result = await _context.WatchBrands.FirstOrDefaultAsync(x => x.BrandName.ToLower().Trim() == watchBrand.ToLower().Trim());
            return result;
        }

        public bool DeleteWatchBrand(WatchBrand watchBrand)
        {
            try
            {
                _context.WatchBrands.Remove(watchBrand);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateWatchBrand(WatchBrand watchBrand)
        {
            try
            {
                _context.WatchBrands.Update(watchBrand);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
