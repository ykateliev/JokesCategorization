using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JokesCategorization.Controllers
{
	public class DataController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> GetData()
        {


			return View();
		}
	}
}