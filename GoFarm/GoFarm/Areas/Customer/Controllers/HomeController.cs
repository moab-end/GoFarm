using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GoFarm.Models;
using GoFarm.Models.ViewModels;
using GoFarm.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using GoFarm.Utility;
using GoFarm.Extensions;

namespace GoFarm.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		private readonly IUnitOfWork _unitOfWork;
		private HomeViewModel HomeVM;

		

		public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			HomeVM = new HomeViewModel() {

				CategoryList = _unitOfWork.Category.GetAll(),
				ServiceList = _unitOfWork.Service.GetAll(includeProperties:"Frequency"),
			
			};

			return View(HomeVM);
		}

		public IActionResult Details(int Id)
		{
			var objFromDb = _unitOfWork.Service.GetFirstOrDefault(includeProperties: "Category,Frequency",filter:c=>c.Id==Id);

			return View(objFromDb);
		}

		public IActionResult AddToCart(int serviceId)
		{
			List<int> sessionList = new List<int>();

			if (string.IsNullOrEmpty(HttpContext.Session.GetString(SD.SessionCart)))
			{
				sessionList.Add(serviceId);
				HttpContext.Session.SetObject(SD.SessionCart, sessionList);
			}
			else
			{
				sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
				if (!sessionList.Contains(serviceId))
				{
					sessionList.Add(serviceId);
					HttpContext.Session.SetObject(SD.SessionCart,sessionList);
				}
			}

			return RedirectToAction(nameof(Index));
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
