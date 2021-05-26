using System;

namespace AravandTak.Queries.Products
{
	public class ProductDetailAttributeViewModel
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public bool IsAvailable { get; set; }
		public decimal Price { get; set; }
		public string[] Colors { get; set; }
	}
}
