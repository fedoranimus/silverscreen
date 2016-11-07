using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Silverscreen.Core.Library;
using Silverscreen.Core.Wishlist;
using Silverscreen.Core.Parser;
using Silverscreen.Core.Renamer;
using Silverscreen.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Silverscreen.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure CORS
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.WithOrigins("http://localhost:5000");
            corsBuilder.AllowCredentials();
            services.AddCors(options => {
                options.AddPolicy("Silverscreen", corsBuilder.Build());
            });

            // Add framework services.
            services.AddMvc();

            var connection = @"Server=(localdb)\mssqllocaldb;Database=Silverscreen;Trusted_Connection=True;";
            services.AddDbContext<MediaCollectionContext>(options => options.UseSqlServer(connection));

            services.TryAddSingleton<IParserService, ParserService>();
            services.TryAddSingleton<ILibraryService, LibraryService>();
            services.TryAddSingleton<IWishlistService, WishlistService>();
            services.TryAddSingleton<IRenamerService, RenamerService>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors("Silverscreen");
            
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
