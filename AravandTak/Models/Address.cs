using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AravandTak.Models
{
	public class Address
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[Column(TypeName = "NVARCHAR(500)")]
		public string Line1 { get; set; }
		public long? PostalCode { get; set; }

		public virtual ArvandTakUser User { get; set; }
	}
}
