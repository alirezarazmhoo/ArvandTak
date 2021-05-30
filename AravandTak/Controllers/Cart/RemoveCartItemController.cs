using AravandTak.Data;
using AravandTak.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AravandTak.Controllers.Cart
{
	[Route("cart")]
	public class RemoveCartItemController : BaseController
	{
		public RemoveCartItemController(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		[Route("removeItem/{id}")]
		[HttpPost]
		public IActionResult Remove(Guid id)
		{
			var index = FindCartItemIndex(id);

			if (index != -1)
			{
				CartItems.RemoveAt(index);
				SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", CartItems);
			}

			return RedirectToAction("Index", "OrderList");
		}
	}
}
