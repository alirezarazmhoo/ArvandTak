using AravandTak.Data;
using AravandTak.Helpers;
using AravandTak.Models.Cart;
using AravandTak.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AravandTak.Controllers.Cart
{
	[Route("cart")]
	public class ItemListController : BaseController
	{
		public ItemListController(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		[HttpGet]
		[Route("/cart/")]
		[Route("/cart/index")]
		public async Task<IActionResult> Index()
		{
			var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
			var items = new List<CartItemViewModel>();

			if (cart != null && cart.Count > 0)
			{
				items = await
					DbContext.ProductAttributes.
						Select(s => new CartItemViewModel
						{
							ProductAttributeId = s.Id,
							FeatureImage = s.Product.FeatureImage,
							Price = s.Price,
							Title = s.Product.Title + " " + s.Title,
							Number = cart.FirstOrDefault(f => f.ProductAttributeId == s.Id).Number,
							SumPrice = cart.FirstOrDefault(f => f.ProductAttributeId == s.Id).Number * s.Price
						})
						.ToListAsync();
			}

			// Redirect to custom view help: https://docs.microsoft.com/en-us/aspnet/core/mvc/views/overview?view=aspnetcore-5.0
			return View("Views/Cart/Index.cshtml", items);
		}
	}
}
