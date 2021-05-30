using AravandTak.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AravandTak.Controllers.Cart
{
	[Route("cart")]
	public class ChangeOrderNumberController : BaseController
	{
		public ChangeOrderNumberController(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		[Route("changednumber/{id}")]
		[AcceptVerbs("Post")]
		[HttpPost]
		public IActionResult ChangeNumber(Guid id, int value)
		{
			int index = FindCartItemIndex(id);
			if (index != -1)
			{
				CartItems[index].Number = value;
				return Ok();
			}

			return StatusCode(404);
		}
	}
}
