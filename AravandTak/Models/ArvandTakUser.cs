using Microsoft.AspNetCore.Identity;

namespace AravandTak.Models
{
	public class ArvandTakUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string ReferenceUserId { get; set; }
		public int AddressId { get; set; }
		public string Code { get; set; }
	}
}
