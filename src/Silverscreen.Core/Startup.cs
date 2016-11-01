using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Silverscreen.Library;
using Silverscreen.Model;
using Silverscreen.Wishlist;
using Silverscreen.Renamer;
using Microsoft.EntityFrameworkCore;


namespace Silverscreen.Core
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var connection = @"Server=(localdb)\mssqllocaldb;Database=Silverscreen;Trusted_Connection=True;";
            services.AddDbContext<LibraryContext>(options => options.UseSqlServer(connection));

            services.AddSingleton<ILibraryService, LibraryService>();
            services.AddSingleton<IWishlistService, WishlistService>();
            services.AddSingleton<IRenamerService, RenamerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            } 
            else 
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseFileServer(new FileServerOptions 
            {
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = false
            });

            app.UseMvc();

            // app.UseMvc(routes => {
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}
            //    );
            // });
        }
    }
}
