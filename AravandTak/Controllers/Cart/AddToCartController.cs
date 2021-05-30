using AravandTak.Data;
using AravandTak.Helpers;
using AravandTak.Models.Cart;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AravandTak.Controllers.Cart
{
	[Route("cart")]
	public class AddToCartController : BaseController
	{
		public AddToCartController(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		
		private void AddItem(Guid productAttributeId, int number, string color)
		{
			var item = new CartItem
			{
				ProductAttributeId = productAttributeId,
				Number = number,
				Color = color
			};

			CartItems.Add(item);

			SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", CartItems);
		}


		[Route("/cart/store")]
		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public IActionResult Store(CartItem item)
		{
			InitialCart();

			item.Color = string.IsNullOrEmpty(item.Color) ? CartItem.DefaultColor : item.Color;

			// check selected item is valid
			var productAttr = DbContext.ProductAttributes.FirstOrDefault(f => f.Id == item.ProductAttributeId);
			if (productAttr == null) return NotFound();

			// check availibilty 
			if (!productAttr.IsAvailable)
			{
				// set message 
				TempData["message"] = "موجودی بسته بندی انتخاب شده تمام شده است";
				// return to product page
				var productId = productAttr.ProductId;
				return RedirectToAction("index", "ProductDetai", new { id = productId });
			}

			try
			{
				// save to session
				int index = FindCartItemIndex(item.ProductAttributeId);
				if (index != -1)
				{
					CartItems[index].Number += item.Number;
					SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", CartItems);
				}
				else
				{
					AddItem(item.ProductAttributeId, item.Number, item.Color);
				}

				// redirect to cart list page
				return RedirectToAction("index", "OrderList");

			}
			catch (System.Exception)
			{
				// return to product page
				var productId = productAttr.ProductId;
				TempData["message"] = "خطا در ثبت سفارش، لطفا مجددا تلاش کنید";

				return RedirectToAction("index", "ProductDetai", new { id = productId });
			}
		}
	}
}
