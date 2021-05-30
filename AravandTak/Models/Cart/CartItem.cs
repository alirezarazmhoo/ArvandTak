using System;
using System.ComponentModel.DataAnnotations;

namespace AravandTak.Models.Cart
{
	public class CartItem
	{
		public const string DefaultColor = "#FFF";
		[Required]
		public Guid ProductAttributeId { get; set; }
		public string Color { get; set; }
		[Required]
		[Range(1, 99, ErrorMessage = "بین یک تا 99 بسته می توانید سفارش دهید")]
		public int Number { get; set; }
	}
}
