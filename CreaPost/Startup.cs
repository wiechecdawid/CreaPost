using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreaPost.Data;
using CreaPost.Middleware;
using CreaPost.Models;
using CreaPost.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

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
            //services.AddTransient<IEmailSender, EmailSender>();
            //services.Configure<EmailSettings>(options => _configuration.GetSection("EmailSettings").Bind(options));
            services.CustomEmailConfiguration(_configuration);

            services.AddDbContext<CreaPostDbContext>(options => options
                    .UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc();
            services.AddLogging();
            services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(3));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)                           //(cfg =>
            //                                                    {
            //                                                        cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //                                                        cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //                                                    })
                .AddJwtBearer(cfg => 
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = _configuration["Tokens:Issuer"],
                        ValidAudience = _configuration["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]))
                    };
                })
                .AddCookie();
            services.AddTransient<CreaPostSeeder>();
            services.AddIdentity<StoreUser, IdentityRole>(configuration =>
            {
                configuration.User.RequireUniqueEmail = true;
                configuration.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<CreaPostDbContext>()
                .AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
                                ILoggerFactory logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            logger.AddConsole(_configuration.GetSection("Logging"));
            logger.AddDebug();

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

            routeBuilder.MapRoute(
                name: "Contact",
                template: "{controller}/{action}",
                defaults: new { controller = "Contact", action = "Contact" });
        }
    }
}
