using AravandTak.Data;
using AravandTak.Helpers;
using AravandTak.Models.Cart;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AravandTak.Controllers
{
	public abstract class BaseController : Controller
	{
		protected readonly ApplicationDbContext DbContext;
		protected IList<CartItem> CartItems;
		public BaseController(ApplicationDbContext dbContext)
		{
			DbContext = dbContext;
		}

		protected void InitialCart()
		{
			CartItems = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");

			if (CartItems == null)
			{
				List<CartItem> items = new List<CartItem>();
				// create empty list
				SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", items);
				// add created empty list to items
				CartItems = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
			}
		}
		protected int FindCartItemIndex(Guid itemId)
		{
			InitialCart();

			for (int i = 0; i < CartItems.Count; i++)
			{
				if (CartItems[i].ProductAttributeId == itemId)
				{
					return i;
				}
			}

			return -1;
		}
	}
}
