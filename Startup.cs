using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PieShop.Models;

namespace PieShop
{
    public class Startup
    {
        
        public IConfiguration Configuration { get; }

        // Auto passed in with DI
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // register services here through dependency Injection (conatiner so that we don't have tight coupling)

            // register services
            services.AddDbContext<AppDbContext>(options =>
            {
                // options.UseSqlServer(Configuration.GetConnectionString("LocalDockerConnection"));
                options.UseSqlServer(Configuration.GetConnectionString("AzureDemoConnection"));
            });

            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();

            // Configure Identity
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });

            services.AddScoped<IPieRepository, PieRepository>(); // MockPieRepository
            services.AddScoped<ICategoryRepository, CategoryRepository>(); // MockCategoryRepository
            services.AddScoped<IOrderRepository, OrderRepository>(); 
            services.AddScoped<ShoppingCart>(sp => ShoppingCart.GetCart(sp)); 
            services.AddHttpContextAccessor();
            services.AddSession();

            // servcies.AddScoped - Middle ground - An instance is created per request. Remains active for the entire request. When out of scope the instance is discarded. Like a sington per request.
            // services.AddTransient - Gives you back a new instance every time you ask for one
            // services.AddSingleton - Single instance for the entire app that is reused 

            services.AddControllersWithViews(); // Add support for MVC (replaces old services.AddMvc())
            services.AddRazorPages();           // Required for scaffolded Identity pages

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Middleware request pipeline - add middleware components here
            // Intercept incoming http requests and initiate a response

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();  // Auto route to use Https
            app.UseStaticFiles();       // Serve static files such as images, CSS, JavaScript etc (defaults to ...root but we can change this)
            app.UseSession();           // Middleware to use sessions. Must be called before UseRouting 

            app.UseRouting();
            app.UseAuthentication();       // AspNet Identity
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapGet("/", async context =>
                // {
                //     await context.Response.WriteAsync("Hello World!");
                // });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapRazorPages();

            });
        }
    }
}
