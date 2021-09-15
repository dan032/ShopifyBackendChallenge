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
using ShopifyBackendChallenge.Web.Helpers;
using ShopifyBackendChallenge.Web.Services.common;
using ShopifyBackendChallenge.Web.Services.Jwt;

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
            //services.AddCors();
            services.AddControllersWithViews();

            services.AddDbContext<RepoDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddScoped<IImageMetadata, SqlImageMetadata>();
            services.AddScoped<IImageRepo, FolderStorageImageRepo>();
            services.AddScoped<IUserData, SqlUserData>();
            services.AddScoped<IUserAuthentication, JwtUserAuthentication>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetService<RepoDbContext>();
            //    context.Database.Migrate();
            //}

            app.UseRouting();

            //app.UseCors(x => x
            //    .AllowAnyOrigin()
            //    .AllowAnyMethod()
            //    .AllowAnyHeader());
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            SeedData.EnsurePopulated(app);
        }
    }
}
