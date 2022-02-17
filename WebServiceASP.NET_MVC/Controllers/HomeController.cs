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
        public static List<Product> Products { get; set; }

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        



        //Методы на разные операции (пример) // Выполняем запрос на WEB API сервис, получаем ответ, обрабатываем и выводим на View
        //Метод AddProduct(Product product)
        //{   string url = "https://YOUR_COMPANY_HERE.beebole-apps.com/api";
        //      string data = "{\"service\":\"absence.list\", \"company_id\":3}";
        //      WebRequest myReq = WebRequest.Create(url);
        


        public IActionResult Index(string name)//
        {
            //Загрузка всех данных из БД (метод GET)
            //HttpClient httpClient = new HttpClient();
            if(name==null)
            {
                WebRequest request = WebRequest.Create(@"https://localhost:5001/api/Product");
                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader Reader = new StreamReader(stream))
                    {
                        Products = JsonConvert.DeserializeObject<List<Product>>(Reader.ReadToEnd());
                        return View(Products);
                    }
                }
                
            }
            else
            {
                WebRequest request = WebRequest.Create(@$"https://localhost:5001/api/Product/GetByName/{name}");
                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader Reader = new StreamReader(stream))
                    {
                        Products = JsonConvert.DeserializeObject<List<Product>>(Reader.ReadToEnd());
                    }
                }
                return View(Products);
            }
        }

        // GET: MyEntities/Create
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            Product product = Products.FirstOrDefault(x => x.ID == id);
            return PartialView(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                WebRequest webRequest = WebRequest.Create(@"https://localhost:5001/api/Product");
                webRequest.Method = "PUT";
                webRequest.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(product);
                    streamWriter.Write(json);
                }

                var response = webRequest.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }
        
        
        
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.ID = Guid.NewGuid();
                WebRequest webRequest = WebRequest.Create(@"https://localhost:5001/api/Product");
                webRequest.Method = "POST";
                webRequest.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(product);
                    streamWriter.Write(json);
                }

                var response = webRequest.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
            }
            return RedirectToAction("Index");
        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,FirstName,SecondName,PhoneNumber,Email")] MyEntity myEntity)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.MyEntities.Add(myEntity);
        //        db.SaveChanges();
        //        return Json(myEntity);
        //    }
        //    return Json("Create error");
        //}


        // GET: MyEntities/Create
        [HttpGet]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(Guid id)
        {
            Product product = Products.FirstOrDefault(x => x.ID == id);
            return PartialView(product);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            WebRequest webRequest = WebRequest.Create(@$"https://localhost:5001/api/Product/{id}");
            webRequest.Method = "Delete";

            var response = webRequest.GetResponse();

            
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult ProductTable()
        {
            WebRequest request = WebRequest.Create(@"https://localhost:5001/api/Product");
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader Reader = new StreamReader(stream))
                {
                    Products = JsonConvert.DeserializeObject<List<Product>>(Reader.ReadToEnd());
                }
            }
            return PartialView("_ProductTable", Products);
        }




        [HttpPost]
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