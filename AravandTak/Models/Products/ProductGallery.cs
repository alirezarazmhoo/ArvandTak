using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AravandTak.Models.Products
{
	public class ProductGallery
	{
		[Key]
		public Guid Id { get; set; }
		[Required]
		public Guid ProductId { get; set; }
		[Required]
		[Column(TypeName = "NVARCHAR(255)")]
		public string Path { get; set; }

		public virtual Product Product { get; set; }
	}
}
