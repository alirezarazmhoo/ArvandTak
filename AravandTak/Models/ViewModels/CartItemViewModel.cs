using System;

namespace AravandTak.Models.ViewModels
{
	public class CartItemViewModel
	{
		public Guid ProductId { get; set; }
		public Guid ProductAttributeId { get; set; }
		public string FeatureImage { get; set; }
		public string Title { get; set; }
		public decimal Price { get; set; }
		public int Number { get; set; }
		public decimal SumPrice { get; set; }
	}
}
