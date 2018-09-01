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
        private static readonly Random Rnd = new Random();
        // GET: Bikes
        public ActionResult Random()
        {
            var database = new L2Entities1();

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

        public ActionResult Edit(int id)
        {
            return Content("id=" + id);
        }

        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;

            if (String.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";

            return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        }

        [Route("bikes/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}