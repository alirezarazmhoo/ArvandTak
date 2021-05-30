using AravandTak.Queries.Products;
using System.Collections.Generic;

namespace AravandTak.Models.ViewModels
{
	public class SimpleProductDetailViewModel
	{
		public ProductDetailSingleAttributeQuery ProductDetail { get; set; }
		public IEnumerable<RelatedProductQuery> RelatedProducts { get; set; }
	}
}
