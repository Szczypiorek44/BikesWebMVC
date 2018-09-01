using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using L2_podejscie_2.Models;

namespace L2_podejscie_2.Controllers
{
    public class BikesController : Controller
    {
        // GET: Bikes
        public ActionResult Random()
        {
            var bike = new Bike() {Name = "Honda"};
            return View(bike);
        }
    }
}