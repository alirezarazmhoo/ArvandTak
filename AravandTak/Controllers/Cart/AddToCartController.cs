using AravandTak.Data;
using AravandTak.Helpers;
using AravandTak.Models.Cart;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AravandTak.Controllers.Cart
{
	[Route("cart")]
	public class AddToCartController : BaseController
	{
		private readonly IList<CartItem> _items;

		public AddToCartController(ApplicationDbContext dbContext) : base(dbContext)
		{
			_items = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");

			if (_items == null)
			{
				List<CartItem> items = new List<CartItem>();
				// create empty list
				SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", items);
				// add created empty list to items
				_items = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
			}
		}

		private int FindItemIndex(CartItem item)
		{
			for (int i = 0; i < _items.Count; i++)
			{
				if (_items[i].ProductAttributeId == item.ProductAttributeId)
				{
					return i;
				}
			}

			return -1;
		}
		private void AddItem(Guid productAttributeId, int number, string color)
		{
			var item = new CartItem
			{
				ProductAttributeId = productAttributeId,
				Number = number,
				Color = color
			};

			_items.Add(item);

			SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", _items);
		}


		[Route("/cart/store")]
		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public IActionResult Store(CartItem item)
		{
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
				int index = FindItemIndex(item);
				if (index != -1)
				{
					_items[index].Number += item.Number;
				}
				else
				{
					AddItem(item.ProductAttributeId, item.Number, item.Color);
				}

				// redirect to cart list page
				return RedirectToAction("Index");

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
