using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;


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
                builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(
                options=>options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(4))

                .AddDefaultTokenProviders().AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();


             
            builder.Services.AddSingleton<IEmailSender, EmailSender>(); 
            

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

            app.UseAuthorization();
            app.UseAuthorization();

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
