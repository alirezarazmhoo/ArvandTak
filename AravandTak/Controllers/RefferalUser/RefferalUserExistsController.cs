using AravandTak.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AravandTak.Controllers.RefferalUser
{
	public class RefferalUserExistsController : BaseController
	{

		public RefferalUserExistsController(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<IActionResult> Index(string code)
		{
			bool result;
			result = await DbContext.Users.AnyAsync(p => p.Code == code);

			return Json(result);
		}
	}
}
