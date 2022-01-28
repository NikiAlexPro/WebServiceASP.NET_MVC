using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebServiceASP.NET_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace WebServiceASP.NET_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //ApplicationContext context;
        //public HomeController(ApplicationContext db)
        //{
        //    context = db;
        //}

        //Методы на разные операции (пример) // ВЫполняем запрос на WEB API сервис, получаем ответ, обрабатываем и выводим на View
        //Метод AddProduct(Product product)
        //{   string url = "https://YOUR_COMPANY_HERE.beebole-apps.com/api";
        //      string data = "{\"service\":\"absence.list\", \"company_id\":3}";
        //      WebRequest myReq = WebRequest.Create(url);
    


        public IActionResult Index()
        {
            return View();
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