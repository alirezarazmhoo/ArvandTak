using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AravandTak.Models.Products
{
	public class ProductSpecification
	{
		[Key]
		public Guid Id { get; set; }
		[Required]
		public Guid ProductId { get; set; }
		[Required]
		[Column(TypeName = "NVARCHAR(200)")]
		public string Key { get; set; } = "---";
		[Required]
		[Column(TypeName = "NVARCHAR(300)")]
		public string Value { get; set; }

		public virtual Product Product { get; set; }
	}
}
