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

		public DbSet<Address> Addresses { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// user and refferal user
			builder.Entity<ArvandTakUser>()
						.HasOne(x => x.ParentRefferalUser)
						.WithMany(x => x.ChildernRefferalUsers)
						.HasForeignKey(x => x.RefferalUserId);
		}
	}
}
