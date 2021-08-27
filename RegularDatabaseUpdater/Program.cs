using Microsoft.EntityFrameworkCore;
using System;
using VsePikchi.Data;

namespace RegularDatabaseUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            var options = optionsBuilder
                    .UseSqlServer(@"Server=localhost\\SQLEXPRESS;Database=VsePikchi;Trusted_Connection=True;MultipleActiveResultSets=True")
                    .Options;

            using (AppDbContext db = new AppDbContext(options))
            {
                foreach (var picture in db.Pictures)
                {
                    db.Pictures.Remove(picture);
                }
                db.SaveChanges();
            }
        }
    }
}
