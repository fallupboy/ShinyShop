using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShinyShop.Models;
using ShinyShop.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ShinyShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProfileRepository _repoProfile;

        public HomeController(IProfileRepository repoProfile)
        {
            _repoProfile = repoProfile;
        }

        public async Task<IActionResult> Index()
        {
            User currUser = await _repoProfile.GetCurrentUser(User);
            return View(currUser);
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
