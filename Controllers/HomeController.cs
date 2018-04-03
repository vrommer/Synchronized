using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Synchronized.Core.Interfaces;
using Synchronized.WebApp.Models;

namespace Synchronized.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuestionService questionService;

        public HomeController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 20;
            var homeViewModel = questionService.GetHomeViewModel(page ?? 1, pageSize);

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
