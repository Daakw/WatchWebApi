using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
    public class DataContext : DbContext

    {
        public DataContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<WatchBrand> WatchBrands { get; set; }
        public DbSet<WatchModel> WatchModels { get; set; }
    }
}
