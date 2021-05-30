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
	public class OrderListController : BaseController
	{
		public OrderListController(ApplicationDbContext dbContext) : base(dbContext)
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
				try
				{
					var ids = cart.Select(s => s.ProductAttributeId).ToList();

					items = await
								DbContext.ProductAttributes
								.Where(w => ids.Contains(w.Id))
								.Select(s => new CartItemViewModel
								{
									ProductId = s.ProductId,
									ProductAttributeId = s.Id,
									FeatureImage = s.Product.FeatureImage,
									Price = s.Price,
									Title = s.Product.Title + " " + s.Title
								})
								.ToListAsync();

					foreach (var item in items)
					{
						item.Number = cart.FirstOrDefault(f => f.ProductAttributeId == item.ProductAttributeId).Number;
						item.SumPrice = cart.FirstOrDefault(f => f.ProductAttributeId == item.ProductAttributeId).Number * item.Price;
					}

					//var Number = cart.FirstOrDefault(f => f.ProductAttributeId == s.Id).Number,
					//				SumPrice = cart.FirstOrDefault(f => f.ProductAttributeId == s.Id).Number * s.Price
				}
				catch (System.Exception ex)
				{

					throw;
				}

			}

			// Redirect to custom view help: https://docs.microsoft.com/en-us/aspnet/core/mvc/views/overview?view=aspnetcore-5.0
			return View("Views/Cart/Index.cshtml", items);
		}
	}
}
