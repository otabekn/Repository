using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Examples.Models;
using ServiceList;
using MongoDB.Bson;

using Entity;
using GenericControllera;

namespace Examples.Controllers
{
    public class HomeController : GenericController<Data, string>
    {
        IDataService _data;
        public HomeController(IDataService data):base(data) {
            _data = data;
        }
        public IActionResult Index()
        {
            _data.Add(new Entity.Data() { Id = ObjectId.GenerateNewId().ToString(), Name = "sd" });
            return Ok();
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
