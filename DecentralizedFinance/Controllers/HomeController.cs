using DecentralizedFinance.Common.ExtensionMethods;
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
                    Price = ((double)t.Price).ToString("#,0"),//t.Price.ToString(),
                    //FullName = t.FullName,
                    TotalAmount = ((double)t.TotalAmount).ToString("#,0"),
                    Tvl = t.Tvl,
                    GroupName = t.Group.Name,
                })
                .ToListAsync();


            var maxProjectId = db.DeFiProject.Max(p => p.Id);
            var lastProject = await db.DeFiProject
                .Where(p => p.Id == maxProjectId)
                .Select(p => new DefiProjectViewModel
                {
                    DefiTotalCash = p.DefiTotalCash.ToString("#,0"),
                    TotalValueLocked = p.TotalValueLocked.ToString("#,0"),
                    MakerDominance = p.MakerDominance.ToString("#,0"),
                    DeFiPulseIndex = p.DeFiPulseIndex.ToString("#,0"),
                    RegisterDate = p.RegisterDate.ToShamsi(),
                })
                .FirstOrDefaultAsync();

            var homeViewModel = new HomeViewModel
            {
                Tokens = tokens,
                LastDeFiProject = lastProject,
            };

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
