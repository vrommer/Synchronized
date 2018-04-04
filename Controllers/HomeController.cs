using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Synchronized.Core.Interfaces;
using Synchronized.WebApp.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Synchronized.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly ILogger _logger;

        public HomeController(
            IQuestionService questionService,
            ILogger<HomeController> logger
            )
        {
            _questionService = questionService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int? page, int? pageNumber)
        {
            ViewBag.maxPage = ViewBag.maxPage == null ? 1 : ViewBag.maxPage;      
            var currentPage = pageNumber ?? (page ?? 1 );
            int pageSize = 20;
            var homeViewModel = await _questionService.GetHomeViewModel(currentPage, pageSize);

            return View(homeViewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
