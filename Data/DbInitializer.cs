using System;
using System.Linq;
using aspnetcore.Data;
using aspnetcore.Models;
using aspnetcore.Services;
using Microsoft.AspNetCore.Identity;

namespace aspnetcore.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //This example just creates an Administrator role and one Admin users
        public async void Initialize()
        {
            //create database schema if none exists
            _context.Database.EnsureCreated();

            //If there is already an Administrator role, abort
            if (_context.Roles.Any(r => r.Name == "Admin")) return;

            //Create the Administartor Role
            await _roleManager.CreateAsync(new IdentityRole("Admin"));

            string username = "mojoblanco";
            string email = "mojoblanco1@gmail.com";
            string password = "Password@9";
            //Create the default Admin account and apply the Administrator role
            var user = new ApplicationUser
            {
                Name = "Mojo Blanco",
                DOB = DateTime.Parse("10/10/2010"),
                UserName = username,
                NormalizedUserName = username.ToUpper(),
                Email = email,
                NormalizedEmail = "mojoblanco@gmail.com".ToUpper(),
                EmailConfirmed = true,
            };
            
            await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(user.UserName), "Admin");
        }
    }
}