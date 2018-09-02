using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using L2_podejscie_2.ViewModels;

namespace L2_podejscie_2.Controllers
{
    public class BikesController : Controller
    {
        private L2Entities1 database = new L2Entities1();

        private static readonly Random Rnd = new Random();

        public ActionResult Random()
        {
            var bikes = database.Bikes.ToList();
            var randomId = Rnd.Next(bikes.Count);
            var bike = bikes[randomId];

            var transactions = database.Transactions.ToList();
            var customers = new List<Customer>();
            foreach (var transaction in transactions)
            {
                if (transaction.BikeId == bike.Id)
                    customers.Add(transaction.Customer);
            }


            var viewModel = new RandomBikeViewModel
            {
                Bike = bike,
                Customers = customers
            };

            return View(viewModel);

        }
        public ActionResult Show(int id)
        {
            var bikes = database.Bikes.ToList();
            var viewModel = new ShowBikeViewModel();

            var bike = bikes.Find(predicate => predicate.Id == id);

            if (bike == null)
                return View();

            var transactions = database.Transactions.ToList();
            var customers = new List<Customer>();
            foreach (var transaction in transactions)
            {
                if (transaction.BikeId == bike.Id)
                    customers.Add(transaction.Customer);
            }

            viewModel.Bike = bike;
            viewModel.Customers = customers;

            return View(viewModel);
        }

        public ActionResult Index()
        {
            var bikes = database.Bikes.ToList();

            var viewModel = new IndexBikeViewModel()
            {
                Bikes = bikes
            };

            return View(viewModel);
        }

        [Route("bikes/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        [HttpPost]
        public ActionResult AuthorizeBuy()
        {
            var customerId = Session["customerId"];
            if (customerId == null)
                return RedirectToAction("Login", "Customer");
            else
            {
                using (L2Entities1 db = new L2Entities1())
                {
                    var transaction = new Transaction()
                    {
                        CustomerId = int.Parse(customerId.ToString()),
                        BikeId = (int)Session["bikeId"],
                        Date = DateTime.UtcNow.Date
                    };
                    db.Transactions.Add(transaction);
                    db.SaveChanges();
                    Session["bikeId"] = null;
                    return RedirectToAction("BuySuccess", "Bikes");
                }
            }
            
        }

        public ActionResult BuySuccess()
        {
            return View();
        }
    }
}