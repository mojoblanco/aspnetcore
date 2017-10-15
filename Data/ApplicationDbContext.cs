using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using aspnetcore.Models;
using Microsoft.AspNetCore.Identity;

namespace aspnetcore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");

			//Shorten key length for Identity
			builder.Entity<ApplicationUser>(entity => {
				entity.Property(m => m.Email).HasMaxLength(127);
				entity.Property(m => m.NormalizedEmail).HasMaxLength(127);
				entity.Property(m => m.NormalizedUserName).HasMaxLength(127);
				entity.Property(m => m.UserName).HasMaxLength(127);
				entity.Property(m => m.Name).HasMaxLength(127);
			});
			builder.Entity<IdentityRole>(entity => {
				entity.Property(m => m.Name).HasMaxLength(127);
				entity.Property(m => m.NormalizedName).HasMaxLength(127);
			});
			builder.Entity<IdentityUserLogin<string>>(entity =>
			{
				entity.Property(m => m.LoginProvider).HasMaxLength(127);
				entity.Property(m => m.ProviderKey).HasMaxLength(127);
			});
			builder.Entity<IdentityUserRole<string>>(entity =>
			{
				entity.Property(m => m.UserId).HasMaxLength(127);
				entity.Property(m => m.RoleId).HasMaxLength(127);
			});
			builder.Entity<IdentityUserToken<string>>(entity =>
			{
				entity.Property(m => m.UserId).HasMaxLength(127);
				entity.Property(m => m.LoginProvider).HasMaxLength(127);
				entity.Property(m => m.Name).HasMaxLength(127);

			});
        }
    }
}
