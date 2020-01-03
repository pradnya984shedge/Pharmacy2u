using MVC_New.Models;
using MVC_New.Services;
using MVC_New.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_New.Controllers
{
    public class HomeController : Controller
    {
        public readonly ICurrencyService _currentService;

        public HomeController()
        {
            _currentService = new CurrencyService();
        }

        //public HomeController(ICurrencyService currencyService)
        //{
        //    _currentService = currencyService;
        //}

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Currency()
        {
            return View("Currency");
        }

        [HttpPost]
        public ActionResult Currency(CurrencyViewModel curr)
        {

            if (curr.Amount == 0)
            {
                ModelState.AddModelError("Amount", "Amount is required");
            }

            if (string.IsNullOrEmpty(curr.Currency))
            {
                ModelState.AddModelError("Currency", "Currency is required");
            }

            Double calrate = 0;

            if (ModelState.IsValid)
            {
                calrate = _currentService.CalculateCurrency(curr);

                var vm = new CurrencyRateViewModel()
                {
                    Amount= curr.Amount,
                    CalRateAmount = calrate,
                    currency = curr.Currency
                };

                //TempData["Name"] = vm.Name;
                return View("CurrencyDisplayRate",vm);
            }

            return View(curr);
        }
    }
}