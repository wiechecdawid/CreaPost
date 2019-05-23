using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreaPost.Data;
using CreaPost.Models;
using CreaPost.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CreaPost
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddSingleton<IOwner, Owner>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<CreaPostDbContext>(options => options
                    .UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc();
            services.AddTransient<CreaPostSeeder>();
            services.AddIdentity<StoreUser, IdentityRole>(configuration =>
            {
                configuration.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<CreaPostDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
                                ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseNodeModues(env.ContentRootPath);
            app.UseAuthentication();
            app.UseMvc(ConfigureRoutes);
            
                    //app.Run(async (ctx) =>
                    //{
                    //    await ctx.Response.WriteAsync("hello world!");
                    //});

        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", 
                                "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
