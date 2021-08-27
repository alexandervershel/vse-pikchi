using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkPicturesParser;
using VsePikchi.Data;
using VsePikchi.Models;

namespace VsePikchi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();

            services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));

            services.AddHangfireServer();
        }

        public void Configure(IApplicationBuilder app, IBackgroundJobClient backgroundJobs, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseHangfireDashboard();

            RecurringJob.AddOrUpdate("DownloadNewPictures", () => UpdateDatabase(), "0 * * * *");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHangfireDashboard();
            });
        }

        public async Task UpdateDatabase()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                .Options;

            DateTime now = DateTime.Now;
            using (AppDbContext db = new AppDbContext(options))
            {
                PicturesExtractor picturesExtractor = new PicturesExtractor();
                List<string> urls = await picturesExtractor.ExtractAsync();
                foreach (var picture in urls)
                {
                    if (await db.Pictures.FirstOrDefaultAsync(p => p.Url == picture) == null &&
                        await db.CensoredPictures.FirstOrDefaultAsync(p => p.Address == picture) == null)
                    {
                        db.Pictures.Add(new Picture() { Url = picture, CreationDate = now });
                    }
                }

                db.SaveChanges();
            }
        }
    }
}