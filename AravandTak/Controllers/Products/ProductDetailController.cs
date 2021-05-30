using AravandTak.Data;
using AravandTak.Models.ViewModels;
using AravandTak.Queries.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AravandTak.Controllers.Products
{
	[Route("/products/detail")]
	public class ProductDetailController : BaseController
	{
		public ProductDetailController(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		[HttpGet]
		[Route("/products/detail/{id}")]
		public async Task<IActionResult> Index(Guid id)
		{
			// check if id exists
			var exists = await
				DbContext.Products.Include(p => p.Attributes).AnyAsync(p => p.Id == id);

			if (!exists) return NotFound();

			#region Product With Many Attributes Query
			//var productDetail = await
			//	DbContext.Products
			//		.Select(s => new ProductDetailQuery
			//		{
			//			Id = s.Id,
			//			Title = s.Title,
			//			Code = s.Code,
			//			FeatureImage = s.FeatureImage,
			//			Tags = s.TagCollection,
			//			Description = s.Description,
			//			// galleries
			//			Galleries = s.Galleries.Select(x => x.Path).ToArray(),
			//			// attributes
			//			Attributes =
			//				s.Attributes
			//					.OrderBy(x => x.Packing)
			//					.Select(x => new ProductDetailAttributeViewModel
			//					{
			//						Id = x.Id,
			//						Title = x.Title,
			//						IsAvailable = x.IsAvailable,
			//						Price = x.Price,
			//						Colors = x.ColorCollection
			//					})
			//					.Where(w => w.IsAvailable),
			//			// specifications
			//			Specifications =
			//				s.Specifications
			//					.Select(x => new ProductDetailSpecificationViewModel { Key = x.Key, Value = x.Value })
			//		})
			//		.FirstOrDefaultAsync(f => f.Id == id);
			#endregion

			// get prodcut detail
			var productDetail = await
				DbContext.Products
					.Select(s => new ProductDetailSingleAttributeQuery
					{
						Id = s.Id,
						Title = s.Title,
						Code = s.Code,
						FeatureImage = s.FeatureImage,
						Tags = s.TagCollection,
						Description = s.Description,
						// galleries
						Galleries = s.Galleries.Select(x => x.Path).ToArray(),
						// attributes
						Attribute =
							s.Attributes
								.OrderBy(x => x.Packing)
								.Select(x => new ProductDetailAttributeViewModel
								{
									Id = x.Id,
									Title = x.Title,
									IsAvailable = x.IsAvailable,
									Price = x.Price,
									Colors = x.ColorCollection
								})
								.FirstOrDefault(w => w.IsAvailable),
						// specifications
						Specifications =
							s.Specifications
								.Select(x => new ProductDetailSpecificationViewModel { Key = x.Key, Value = x.Value })
					})
					.FirstOrDefaultAsync(f => f.Id == id);

			// get related products
			var relatedProducts = await
				DbContext.Products
					.Select(s => new RelatedProductQuery
					{
						Id = s.Id,
						Title = s.Title,
						FeatureImage = s.FeatureImage,
						Price = s.Attributes.FirstOrDefault(f => f.IsAvailable).Price
					})
					.Where(w => w.Id != id)
					.Take(5)
				.ToListAsync();

			// create moder and pass to view
			var model = new SimpleProductDetailViewModel
			{
				ProductDetail = productDetail,
				RelatedProducts = relatedProducts
			};

			return View(model);
		}
	}
}
