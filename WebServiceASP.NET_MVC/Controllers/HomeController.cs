using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebServiceASP.NET_MVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Newtonsoft.Json;

namespace WebServiceASP.NET_MVC.Controllers
{
    public class HomeController : Controller
    {
        public List<Product> Products { get; set; }

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

        //Методы на разные операции (пример) // Выполняем запрос на WEB API сервис, получаем ответ, обрабатываем и выводим на View
        //Метод AddProduct(Product product)
        //{   string url = "https://YOUR_COMPANY_HERE.beebole-apps.com/api";
        //      string data = "{\"service\":\"absence.list\", \"company_id\":3}";
        //      WebRequest myReq = WebRequest.Create(url);
    


        public IActionResult Index()
        {
            //Загрузка всех данных из БД (метод GET)
            //HttpClient httpClient = new HttpClient();
            
            WebRequest request = WebRequest.Create(@"https://localhost:5001/api/Product");
            WebResponse response = request.GetResponse();
            using(Stream stream = response.GetResponseStream())
            {
                using (StreamReader Reader = new StreamReader(stream))
                {
                    Products = JsonConvert.DeserializeObject<List<Product>>(Reader.ReadToEnd());
                }
            }
            return View(Products);


            
        }

        public IActionResult Test()
        {
            return View("Privacy");
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