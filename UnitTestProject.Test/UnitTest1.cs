using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MVC_New.Controllers;
using MVC_New.Models;
using MVC_New.Services;
using MVC_New.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Web.Mvc;

namespace UnitTestProject.Test
{
    internal class DummySqlException : DbException
    {
        internal DummySqlException(string message) : base(message)
        {
        }
    }

    [TestClass]
    public class UnitTest1
    {        
        private static AuditService _serviceWrapper;
        private static Mock<IAuditService> _mockAuditService;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            _mockAuditService = new Mock<IAuditService>();
            _serviceWrapper = new AuditService(_mockAuditService.Object);
        }

        private readonly List<DateTime> _datetime = new List<DateTime>()
        {
          DateTime.MaxValue,
          DateTime.MinValue,
          new DateTime(2000, 01, 01).AddDays(-1),
          new DateTime(3000, 01, 01).AddDays(1),
        };

        private AuditChanges AuditChanges(string changedBy, DateTime From, DateTime To, double rate, string CurrencyType)
        {         
            if (string.IsNullOrWhiteSpace(changedBy))
                throw new DummySqlException("null/empty/whitespace for user.");
            if (string.IsNullOrWhiteSpace(CurrencyType))
                throw new DummySqlException("null/empty/whitespace for CurrencyType.");
            if (string.IsNullOrWhiteSpace(rate.ToString()) || rate.Equals(0))
                throw new DummySqlException("null/empty/whitespace for rate of currency.");
            return _serviceWrapper.AuditChanges(changedBy, From, To, rate, CurrencyType);
        }

        [TestMethod]
        [DataRow("Test", 1,0, 1.2,"USD", DisplayName = "Successful")]
        public void AddTariffChanges_ForSuccessful(string changedBy, int From, int To, double rate, string CurrencyType)
        {
            DateTime sdate = _datetime[From];
            DateTime edate = _datetime[To];
            AuditChanges tf = AuditChanges(changedBy, sdate, edate, rate, CurrencyType);
            Assert.IsNull(tf);
        }

        [TestMethod]
        [ExpectedException(typeof(DbException), AllowDerivedTypes = true)]
        [DataRow("", 1, 0, 1.2,"USD", DisplayName = "failed for user")]
        [DataRow("test", 1, 0, 0, "AUD", DisplayName = "failed for currency rate")]
        [DataRow("test", 1, 0, 1.2, "", DisplayName = "failed for currency Type")]
        public void AddTariffChanges(string changedBy, int From, int To, double rate, string CurrencyType)
        {
            DateTime sdate = _datetime[From];
            DateTime edate = _datetime[To];

            AuditChanges tf = AuditChanges(changedBy, sdate, edate, rate, CurrencyType);
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnCurrencyView()
        {
            HomeController controller = new HomeController();
            var result = controller.Currency() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Currency", result.ViewName);
        }

        [TestMethod]
        public void Post_CurrencyView_AmountIsZeroOrNull()
        {
            HomeController controller = new HomeController();
            var vm = new CurrencyViewModel()
            {
                Amount = 0,
                Currency = "USD"
            };
            var result = controller.Currency(vm) as ViewResult;
            Assert.IsNotNull(result);

            Assert.IsTrue(controller.ViewData.ModelState.Count == 1, "Amount is required");
        }

        [TestMethod]
        public void Post_CurrencyView_CurrencyIsNull()
        {
            HomeController controller = new HomeController();
            var vm = new CurrencyViewModel()
            {
                Amount = 1,
                Currency = ""
            };
            var result = controller.Currency(vm) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsTrue(controller.ViewData.ModelState.Count == 1, "Currency is required");
        }


        [TestMethod]
        public void Post_CurrencyView_USD()
        {
            HomeController controller = new HomeController();
            var vm = new CurrencyViewModel()
            {
                Amount = 1,
                Currency = "USD"
            };
            var result = controller.Currency(vm) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("CurrencyDisplayRate", result.ViewName);
        }

        [TestMethod]
        public void Post_CurrencyView_EUR()
        {
            HomeController controller = new HomeController();
            var vm = new CurrencyViewModel()
            {
                Amount = 1,
                Currency = "EUR"
            };
            var result = controller.Currency(vm) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("CurrencyDisplayRate", result.ViewName);
        }

        [TestMethod]
        public void Post_CurrencyView_AUD()
        {
            HomeController controller = new HomeController();
            var vm = new CurrencyViewModel()
            {
                Amount = 1,
                Currency = "AUD"
            };
            var result = controller.Currency(vm) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("CurrencyDisplayRate", result.ViewName);
        }
    }
}
