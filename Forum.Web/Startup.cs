using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Web.Data;
using Forum.Web.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ForumContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ForumDb")));

            services.AddSession();

            services.AddTransient<IRepository<User>, EfRepository<User>>();
            services.AddTransient<IRepository<Theme>, EfRepository<Theme>>();
            services.AddTransient<IRepository<Category>, EfRepository<Category>>();
            services.AddTransient<IRepository<Post>, EfRepository<Post>>();
            services.AddTransient<IRepository<Comment>, EfRepository<Comment>>();

            services.AddSession();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "AdminArea",
                   template: "{area:exists}/{controller=Home}/{action=Index}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "categoriesByTheme",
                    template: "{theme}",
                    defaults: new { controller = "Categories", action = "Index" });
                routes.MapRoute(
                    name: "default",
                    template: "{theme}/{category}",
                    defaults: new { controller = "Posts", action = "Index" });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Profile}/{action=Index}/{id?}"
                    );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Posts}/{action=Edit}/{id}"
                    );
                routes.MapRoute(
                    name: "default",
                    template: "{theme}/{category}/{postid}",
                    defaults: new { controller = "Comments", action = "Index" });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Comments}/{action=Edit}/{id}"
                    );
            });
        }
    }
}
