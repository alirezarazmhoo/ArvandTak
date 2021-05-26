using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AravandTak.Models
{
	public class ArvandTakUser : IdentityUser
	{
		[Required]
		[Column(TypeName = "NVARCHAR(100)")]
		public string FirstName { get; set; }
		[Required]
		[Column(TypeName = "NVARCHAR(200)")]
		public string LastName { get; set; }
		public string RefferalUserId { get; set; }
		[Required]
		public string Code { get; set; }

		public virtual ICollection<Address> Addresses { get; set; }
		public virtual ArvandTakUser ParentRefferalUser { get; set; }
		public virtual HashSet<ArvandTakUser> ChildernRefferalUsers { get; set; }
	}
}
