using AravandTak.Data.Seeders;
using AravandTak.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AravandTak.Data
{
	public class ApplicationDbContext : IdentityDbContext<ArvandTakUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public virtual DbSet<Address> Addresses { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// REALTION: User & RefferalUser
			builder.Entity<ArvandTakUser>()
						.HasOne(x => x.ParentRefferalUser)
						.WithMany(x => x.ChildernRefferalUsers)
						.HasForeignKey(x => x.RefferalUserId);

			// store product tags
			builder.Entity<Product>()
				.Property<string>("TagCollection")
				.HasField("_tags");

			// store product attribute colors
			builder.Entity<ProductAttribute>()
				.Property<string>("ColorCollection")
				.HasField("_colors");


			// create main admin user
			builder.SeedAdmin();
		}
	}
}
