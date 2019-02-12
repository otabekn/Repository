using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Examples.Models;
using ServiceList;
using MongoDB.Bson;
using Serilog;

namespace Examples.Controllers
{
    public class HomeController : Controller
    {
        IDataService _data;
        public HomeController(IDataService data) {
            _data = data;
        }
        public IActionResult Index()
        {
            _data.Add(new Entity.Data() { Id = ObjectId.GenerateNewId().ToString(), Name = "sd" });
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
