using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AjaxDemo.Models;

namespace AjaxDemo.Controllers
{
    public class HomeController : Controller
    {
        private AjaxDemoContext db = new AjaxDemoContext();

        public IActionResult Index()
        {
            return View();
        }

        // When Home/Ajax is requested the text bleow is returned. this is the result used in the success function in our javascript.
        public IActionResult HelloAjax()
        {
            return Content("Hello from the controller!", "text/plain");
        }

        //pass parameters from the client side to the server
        public IActionResult Sum(int firstNumber, int secondNumber)
        {
            return Content((firstNumber + secondNumber).ToString(), "text/plain");
        }

        public IActionResult DisplayObject()
        {
            Destination destination = new Destination("Bangkok", "Thailand", 1);
            return Json(destination);
        }
        public IActionResult DisplayViewWithAjax()
        {
            return View();
        }
        // RandomDestinationList: Parameters passed to actions need to match the name attribute in the corresponding name input tag.
        //destinationcount to generat a list of random destinations named randomDestinationList.
        // we returned the list as a JSON formatted response.
        public IActionResult RandomDestinationList(int destinationCount)
        {
            var randomDestinationList = db.Destinations.OrderBy(r => Guid.NewGuid()).Take(destinationCount);
            return Json(randomDestinationList);
        }
        [HttpPost]
        public IActionResult NewDestination(string newCity, string newCountry)
        {
            Destination newDestination = new Destination(newCity, newCountry);

            db.Destinations.Add(newDestination);
            db.SaveChanges();
            return Json(newDestination);
        }
    }


}
