using System;

namespace AravandTak.Models.Cart
{
	public class CartItem
	{
		public Guid ProductAttributeId { get; set; }
		public string Color { get; set; }
		public int Number { get; set; }
	}
}
