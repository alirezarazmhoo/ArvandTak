using AravandTak.Queries.Products;
using System.Collections.Generic;

namespace AravandTak.Models.ViewModels
{
	public class ProductDetailViewModel
	{
		public ProductDetailQuery ProductDetail { get; set; }
		public IEnumerable<RelatedProductQuery> RelatedProducts { get; set; }
	}
}
