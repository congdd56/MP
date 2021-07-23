using VAS.Dealer.Authentication;
using VAS.Dealer.Models;
using VAS.Dealer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace VAS.Dealer.Controllers
{
    [MPAuthorize]
    public class HomeController : MPBaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryServices _MemoryServices;
        public HomeController(ILogger<HomeController> logger, IMemoryServices memoryServices)
        {
            _logger = logger;
            _MemoryServices = memoryServices;
        }

        public IActionResult Index()
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
