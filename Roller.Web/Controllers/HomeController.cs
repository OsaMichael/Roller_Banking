using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Roller.DataContext.Entity;
using Roller.Repository.AccountRepo;
using Microsoft.AspNetCore.Authorization;
using Roller.Web.Models;
using Roller.DContext;
using System.Web.Http;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace Roller.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountManager _accountManager;
        private readonly RollerDataContext _context;
        private readonly UserManager<User> _userManager;

        public HomeController(IAccountManager accountManager, RollerDataContext context,
            UserManager<User> userManager)
        {
            _accountManager = accountManager;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Home()
        {
            return View();
        }

        //[Authorize(Policy = Claims.Admin)]
        public async Task<IActionResult> Index()
        {
            var you = new BankStatisticsViewModel();

            you.TotalAccounts = await _userManager.Users.AsNoTracking().CountAsync();
            you.TotalAccounts = await _context.Accounts.AsNoTracking().CountAsync();
            you.TotalCustomers = await _context.Customers.AsNoTracking().CountAsync();
            you.TotalBalance = await _context.Accounts.AsNoTracking().SumAsync(a => a.Balance ?? 0);

            return View(you);
        }

        //[HttpGet]
        public IActionResult GetSignView()
        {
            return View();
        }
        [Authorize(Policy = Claims.User)]
        public IActionResult IndexUsers()
        {
            //var user = 

            return View();
        }
        [Authorize(Policy = Claims.Cashier)]
        public IActionResult IndexCashier()
        {
            //var user = 

            return View();
        }
        public IActionResult SignOut()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
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
