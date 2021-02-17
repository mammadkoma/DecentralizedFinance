using DecentralizedFinance.Data;
using DecentralizedFinance.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DecentralizedFinance.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext db;

        public HomeController(AppDbContext appDbContext)
        {
            db = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var tokens = await db.Token
                .Include(i => i.Group)
                .Select(t => new TokenViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Price = t.Price.ToString(),
                    //FullName = t.FullName,
                    TotalAmount = t.TotalAmount.ToString(),
                    Tvl = t.Tvl,
                    GroupName = t.Group.Name,
                })
                .ToListAsync();

            foreach (var item in tokens)
            {
                item.Price = double.Parse(item.Price).ToString("#,0");
                item.TotalAmount = double.Parse(item.TotalAmount).ToString("#,0");
            }

            var homeViewModel = new HomeViewModel { Tokens = tokens };

            return View(homeViewModel);
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
