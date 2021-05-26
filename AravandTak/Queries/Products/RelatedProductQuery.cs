using System;

namespace AravandTak.Queries.Products
{
	public class RelatedProductQuery
	{
		public Guid Id { get; set; }
		public string FeatureImage { get; set; }
		public string Title { get; set; }
		public decimal Price { get; set; }
	}
}
