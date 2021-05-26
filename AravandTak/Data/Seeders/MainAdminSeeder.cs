using AravandTak.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace AravandTak.Data.Seeders
{
	public static class MainAdminSeeder
	{
		public const string AdminRoleName = "admin";
		public const string AdminRoleId = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";
		public const string CustomerRoleId = "20df3014-a5e0-4f94-bf15-11685f5f9a85";
		public const string CustomerRoleName = "customer";

		public const string MainAdminId = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
		public static void SeedAdmin(this ModelBuilder builder)
		{
			var userId = MainAdminId;
			var adminUser = new ArvandTakUser
			{
				Id = userId,
				FirstName = "ساره",
				LastName = "صفری",
				PhoneNumber = "09388244318",
				UserName = "mainadmin",
				NormalizedUserName = "mainadmin",
				Email = "mainadmin@email.com",
				NormalizedEmail = "mainadmin@email.com",
				EmailConfirmed = true,
				Code = "111",
				LockoutEnabled = false,
				SecurityStamp = Guid.NewGuid().ToString(),
			};

			var ph = new PasswordHasher<ArvandTakUser>();
			adminUser.PasswordHash = ph.HashPassword(adminUser, "123456");

			builder.Entity<ArvandTakUser>().HasData(adminUser);

			builder.Entity<IdentityRole>().HasData(
				new IdentityRole { Name = "admin", NormalizedName = "admin", Id = AdminRoleId, ConcurrencyStamp = AdminRoleId },
				new IdentityRole { Name = "customer", NormalizedName = "customer", Id = CustomerRoleId, ConcurrencyStamp = CustomerRoleId }
			);

			builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
			{
				RoleId = AdminRoleId,
				UserId = userId
			});
		}
	}
}
