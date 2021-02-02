using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Annonce_Asp.netCore.Models;
using Annonce_Asp.netCore.Models.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Annonce_Asp.netCore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration )
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<Repository<Categories>, Rep_Cat>();
            services.AddScoped<Repository<Ville>, Rep_Ville>();
            services.AddScoped<Repository<Region>, Rep_Region>();
            services.AddScoped<Repository<Annonces>, Rep_Annonces>();
            services.AddScoped<Repository<Latest_News>, Rep_News>();
            services.AddIdentity<AppUsers, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>().
                AddDefaultTokenProviders();
            services.AddDbContext<ApplicationDbContext>(
               option => option.UseSqlServer(Configuration.GetConnectionString("Conx_Annonce"))
               );
            services.ConfigureApplicationCookie(option => option.LoginPath = "/Account/login");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc(route => {
                route.MapRoute("default", "{controller=Home}/{action=Index}/{id?}/{id1?}");
            });
        }
    }
}
