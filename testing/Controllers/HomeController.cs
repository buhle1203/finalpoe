using System.Data.SqlTypes;
using testing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Akka.Event;

namespace testing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly disasteralleviationfoundationdbContext contextDB;

        public HomeController(ILogger<HomeController> logger, disasteralleviationfoundationdbContext bg)
        {
            contextDB = bg;
            _logger = logger;
        }


        public IActionResult Index()
        {
            var moneyLeft = contextDB.DonationsOfMoneys.Select(s=>s.DonationAmount).Sum() - contextDB.AllocationOfMoneys.Select(s=>s.AmountAllocated).Sum() - contextDB.PurchasesOfGoods.Select(a=>a.AmountRequired).Sum();
            ViewBag.moneyLeft = moneyLeft;

            return View();
        }

        
        public IActionResult publicInfoMoney()
        {
            var moneyLeft = contextDB.DonationsOfMoneys.Select(s => s.DonationAmount).Sum() - contextDB.AllocationOfMoneys.Select(s => s.AmountAllocated).Sum() - contextDB.PurchasesOfGoods.Select(a => a.AmountRequired).Sum();
            ViewBag.moneyLeft = moneyLeft;
            var goods = contextDB.DonationOfGoods.Select(s => s.DonationNumberOfItems).Sum() - contextDB.AllocationOfGoods.Select(s => s.NumberOfItemsAllocated).Sum() + contextDB.PurchasesOfGoods.Select(a => a.NumberOfItemsPurchased).Sum();
            ViewBag.goodsLeft = goods;
            IEnumerable<AllocationOfMoney> disasters = contextDB.AllocationOfMoneys.Include(d=>d.Disaster);
            return View(disasters);
            
        }
        

        public IActionResult publicInfoGoods()
        {
            var moneyLeft = contextDB.DonationsOfMoneys.Select(s => s.DonationAmount).Sum() - contextDB.AllocationOfMoneys.Select(s => s.AmountAllocated).Sum() - contextDB.PurchasesOfGoods.Select(a => a.AmountRequired).Sum();
            ViewBag.moneyLeft = moneyLeft;
            var goods = contextDB.DonationOfGoods.Select(s => s.DonationNumberOfItems).Sum() - contextDB.AllocationOfGoods.Select(s => s.NumberOfItemsAllocated).Sum() + contextDB.PurchasesOfGoods.Select(a => a.NumberOfItemsPurchased).Sum();
            ViewBag.goodsLeft = goods;
            IEnumerable<AllocationOfGood> disasters = contextDB.AllocationOfGoods.Include(d => d.Disaster).Include(d => d.DonationOfGoodsCategory);
            return View(disasters);

        }

        public IActionResult AuthorisedUsersLogout()
        {
             Response.Cookies.Delete("Authenticated", new CookieOptions()
            {
                Secure = true,
            });
            return RedirectToAction("Index");
        }

        public IActionResult AuthorisedUsersLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AuthorisedUsersLogin(AuthorisedUser objUser)
        {
            var obj = contextDB.AuthorisedUsers.Where(a => a.UserNames.Equals(objUser.UserNames) && a.UserPassword.Equals(objUser.UserPassword)).FirstOrDefault();  
            if (obj != null)  
            {  
                HttpContext.Response.Cookies.Append("Authenticated", "True"); 
                return RedirectToAction("Index");  
            } 
            return View();
        }

        public IActionResult AuthorisedUsersRegister()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AuthorisedUsersRegister(AuthorisedUser objUser)
        {
            objUser.UserId = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return View(objUser);
            }else{
                contextDB.AuthorisedUsers.Add(objUser);
                contextDB.SaveChanges();
                return RedirectToAction("AuthorisedUsersLogin");
            }
            return View(objUser);
        }

        public IActionResult MakeAMoneyAllocation()
        {
            ViewData["DisasterId"] = new SelectList(contextDB.ActiveDisasters, "DisasterId", "DisasterLocation");
            return View("MakeAMoneyAllocation");
        }

        [HttpPost]
        public IActionResult MakeAMoneyAllocation(AllocationOfMoney money)
        {
            money.AllocationId = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return View(money);
            }else{
                contextDB.AllocationOfMoneys.Add(money);
                contextDB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(money);
        }

        public IActionResult MakeAMoneyDonation()
        {
            //created a make money donation
            return View("MakeAMoneyDonation");
        }

        [HttpPost]
        public IActionResult MakeAMoneyDonation(DonationsOfMoney money)
        {

            money.DonationId = Guid.NewGuid();
            if(!ModelState.IsValid){
                return View(money);
            }else{
                if (money.DonationDonor == null)
                {
                    money.DonationDonor = "Anonymous";
                }
                contextDB.DonationsOfMoneys.Add(money);
                contextDB.SaveChanges();
                return RedirectToAction("ViewDonationsOfMoney");
            }
            return View(money);
        }

        public IActionResult MakeAGoodsAllocation()
        {
            ViewData["DonationCategoryId"] = new SelectList(contextDB.DonationOfGoodsCategories, "DonationCategoryId", "DonationCategoryName");
            ViewData["DisasterId"] = new SelectList(contextDB.ActiveDisasters, "DisasterId", "DisasterLocation");
            return View();
        }

        [HttpPost]
        public IActionResult MakeAGoodsAllocation(AllocationOfGood money)
        {
            money.AllocationId = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return View(money);
            }else{
                contextDB.AllocationOfGoods.Add(money);
                contextDB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(money);
        }

        public IActionResult MakeAPurchase()
        {
            ViewData["DonationCategoryId"] = new SelectList(contextDB.DonationOfGoodsCategories, "DonationCategoryId", "DonationCategoryName");
            ViewData["DisasterId"] = new SelectList(contextDB.ActiveDisasters, "DisasterId", "DisasterLocation");
            return View();
        }

        [HttpPost]
        public IActionResult MakeAPurchase(PurchasesOfGood purchase)
        {
            // capture purchase of goods
            purchase.PurchaseId = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return View(purchase);
            }else{
                contextDB.PurchasesOfGoods.Add(purchase);
                contextDB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(purchase);
        }

        public IActionResult MakeAGoodsDonation()
        {
            ViewData["DonationCategoryId"] = new SelectList(contextDB.DonationOfGoodsCategories, "DonationCategoryId", "DonationCategoryName");
            return View();
        }

        [HttpPost]
        public IActionResult MakeAGoodsDonation(DonationOfGood goods)
        {
            goods.DonationId = Guid.NewGuid();
            if(!ModelState.IsValid){
                return View(goods);
            }else{
                if (goods.DonationDonor == null)
                {
                    goods.DonationDonor = "Anonymous";
                }
                contextDB.DonationOfGoods.Add(goods);
                contextDB.SaveChanges();
                return RedirectToAction("ViewDonationsOfGoods");
            }
            return View(goods);
        }

        public IActionResult AddANewActiveDisaster()
        {
            return View("AddANewActiveDisaster");
        }

        [HttpPost]
        public IActionResult AddANewActiveDisaster(ActiveDisaster disaster)
        {
            //created 
            disaster.DisasterId = Guid.NewGuid();
            if(!ModelState.IsValid){
                return View(disaster);
            }else{
                contextDB.ActiveDisasters.Add(disaster);
                contextDB.SaveChanges();
                return RedirectToAction("ViewActiveDisasters");
            }
            return View(disaster);
        }

        public IActionResult AddANewCategoryOfGoods()
        {
            return View("AddANewCategoryOfGoods");
        }

        [HttpPost]
        public IActionResult AddANewCategoryOfGoods(DonationOfGoodsCategory category)
        {
            category.DonationCategoryId = Guid.NewGuid();
            if(!ModelState.IsValid){
                return View(category);
            }else{
                contextDB.DonationOfGoodsCategories.Add(category);
                contextDB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult ViewDonationsOfMoney()
        {
            IEnumerable<DonationsOfMoney> money = contextDB.DonationsOfMoneys;
            return View(money);
        }

        public IActionResult ViewDonationsOfGoods()
        {
            IEnumerable<DonationOfGood> goods = contextDB.DonationOfGoods.Include(obj => obj.Category);;
            return View(goods);
        }

        public IActionResult ViewActiveDisasters()
        {
            // created an active disaster
            IEnumerable<ActiveDisaster> disasters = contextDB.ActiveDisasters;
            return View(disasters);
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
