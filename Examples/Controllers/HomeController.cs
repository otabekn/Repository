using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Examples.Models;
using ServiceList;
using MongoDB.Bson;

using Entity;


namespace Examples.Controllers
{
    public class HomeController:GenericControllers.GenericController<Data, string> //:// GenericController<Data, string>
    {
        IDataService _data;
        public HomeController(IDataService data):base(data)
        {
            _data = data;
        }
        public IActionResult Index()
        {
            _data.Add(new Entity.Data() { Id = ObjectId.GenerateNewId().ToString(), Name = "sd" });
            return Ok();
        }

        public IActionResult Privacy()
        {
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return Ok();
        }
    }
}
