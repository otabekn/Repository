using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EntityExample.Models;
using ServiceList;

namespace EntityExample.Controllers
{
    public class HomeController : Controller
    {

        IEntityDataService _entity;
        public HomeController(IEntityDataService entity)
        {
            _entity = entity;
        }
        public IActionResult Index()
        {
            _entity.Add(new Entity.EntityData() {   Name="hello world"});
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
