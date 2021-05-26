using System;
using System.Collections.Generic;

namespace AravandTak.Queries.Products
{
	public class ProductDetailQuery
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Code { get; set; }
		public string FeatureImage { get; set; }
		public string[] Tags { get; set; }
		public string Description { get; set; }
		// galleries
		public string[] Galleries { get; set; }
		// attributes
		public IEnumerable<ProductDetailAttributeViewModel> Attributes { get; set; }
		// specifications
		public IEnumerable<ProductDetailSpecificationViewModel> Specifications { get; set; }
	}
}
