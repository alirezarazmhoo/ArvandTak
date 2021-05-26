using AravandTak.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AravandTak.Controllers
{
	public abstract class BaseController : Controller
	{
		protected readonly ApplicationDbContext DbContext;
		public BaseController(ApplicationDbContext dbContext)
		{
			DbContext = dbContext;
		}
	}
}
