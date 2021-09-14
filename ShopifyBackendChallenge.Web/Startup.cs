using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopifyBackendChallenge.Core.User;
using ShopifyBackendChallenge.Data;
using ShopifyBackendChallenge.Data.Common;
using ShopifyBackendChallenge.Data.FileStorage;
using ShopifyBackendChallenge.Data.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace ShopifyBackendChallenge.Web
{
    public class Startup
    {

        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<RepoDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<UserModel, IdentityRole>()
                .AddEntityFrameworkStores<RepoDbContext>();

            services.AddScoped<IImageMetadata, SqlImageMetadata>();
            services.AddScoped<IImageRepo, FolderStorageImageRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseStatusCodePagesWithRedirects("/");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
