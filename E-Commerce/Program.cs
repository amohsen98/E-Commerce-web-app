using E_commerce.DataAccess.Implementation;
using E_commerce.Entities.Repositories;
using E_commerce.Entities.Utility;
using E_Commerce.DataAccess;
using E_Commerce.Entites.Utility;
using E_Commerce.Entities;
using E_Commerce.Entities.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Stripe;


namespace E_Commerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("No Connection String was found");
            

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            //builder.Services.AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(1));
            //Register Database Connection
            builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection"))
            .EnableSensitiveDataLogging() // Enable this for SQL logging
           .LogTo(Console.WriteLine)); // This will log SQL to the console
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.Configure<StripeData>(builder.Configuration.GetSection("stripe"));


            builder.Services.AddIdentity<IdentityUser, IdentityRole>(
                options=>options.Lockout.DefaultLockoutTimeSpan  = TimeSpan.FromDays(4))
                .AddDefaultUI()
                .AddDefaultTokenProviders().AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();


             
            builder.Services.AddSingleton<IEmailSender, EmailSender>();

            builder.Services.AddDistributedMemoryCache();
            //builder.Services.AddSession();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            StripeConfiguration.ApiKey = builder.Configuration.GetSection("stripe:Secretkey").Get<string>();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseSession();
            app.MapRazorPages();

            //app.MapControllerRoute(
            //  name: "default",
            //pattern: "{controller=Home}/{action=Index}/{id?}");

           

            app.MapControllerRoute(
                  name: "default",
                  pattern: "{Area=Admin}/{controller=Home}/{action=Index}/{id?}" );
            app.MapControllerRoute(
                 name: "Customer",
                 pattern: "{Area=Customer}/{controller=Home}/{action=Index}/{id?}");
           
    

            app.Run();
        }
    }
}
