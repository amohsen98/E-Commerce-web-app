using E_Commerce.DataAccess;
using E_Commerce.Entites.Models;
using E_Commerce.Entities.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public DbInitializer(
              UserManager<IdentityUser> userManager,
              RoleManager<IdentityRole> roleManager,
              ApplicationDbContext context)  // Injecting ApplicationDbContext
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;  // Assigning to the field
        }
        public void Initialize()
        {

            //Migration
            try
            {

                if(_context.Database.GetPendingMigrations().Count() > 0)
                {

                    _context.Database.Migrate();
                }


            }
            catch(Exception ex)
            {
                

            }

            //Roles
            if (!_roleManager.RoleExistsAsync(SD.AdminRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.CustomerRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.EditorRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.AdminRole)).GetAwaiter().GetResult();





                //User
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "ahmed@gmail.com",
                    Email = "ahmed@gmail.com",
                    Name = "Administrator",
                    PhoneNumber = "1234567890",
                    Address ="Cairo",
                    City = "Ain Shams",

                },"Ecomm1234!!").GetAwaiter().GetResult();

                ApplicationUser  user = _context.ApplicationUsers.FirstOrDefault(u=>u.Email == "ahmed@gmail.com");

                _userManager.AddToRoleAsync(user, SD.AdminRole).GetAwaiter().GetResult();

            }

            return;

        }
    }
}
