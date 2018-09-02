using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace L2_podejscie_2.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult Login()
        {

            return View();
        }

        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AuthorizeLogin(Customer customer)
        {
            using (L2Entities1 db = new L2Entities1())
            {
                var userDetails = db.Customers.FirstOrDefault(x => x.Login == customer.Login && x.Password ==customer.Password);
                if (userDetails == null)
                {
                    customer.ErrorMessage = "Wrong username or password.";
                    return View("Login", customer);
                }
                else
                {
                    Session["customerId"] = customer.Id;
                    return RedirectToAction("Index", "Bikes");

                }
            }
        }

        [HttpPost]
        public ActionResult AuthorizeRegister(Customer customer)
        {
            using (L2Entities1 db = new L2Entities1())
            {
                var userDetails = db.Customers.FirstOrDefault(x => x.Login == customer.Login);
                if (userDetails != null)
                {
                    customer.ErrorMessage = "Customer with this login already exists.";
                    return View("Register", customer);
                }
                else
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    Session["customerId"] = customer.Id;
                    return RedirectToAction("Index", "Bikes");

                }
            }
        }
    }
}