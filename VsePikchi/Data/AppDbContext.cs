using Microsoft.EntityFrameworkCore;
using VsePikchi.Models;

namespace VsePikchi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<MinPicture> CensoredPictures { get; set; }

    }
}
