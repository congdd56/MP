using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VAS.Dealer.Services;
using VAS.Dealer.Services.Interfaces;

namespace VAS.Dealer.Controllers
{
    public class TestController : Controller
    {
        private readonly IFTPServices _FTPServices;
        private readonly IEmailServices _EmailServices;

        public TestController(IFTPServices fTPServices, IEmailServices EmailServices)
        {
            _FTPServices = fTPServices;
            _EmailServices = EmailServices;
        }

        [HttpGet]
        [Route("Test/notify")]
        public IActionResult notify()
        {
            _EmailServices.SendWarningFtpError("Test");
            return Ok();
        }

        [HttpGet]
        [Route("Test/runregis30minute")]
        public IActionResult runregis30minute()
        {
            _FTPServices.InitDataRegis30(DateTime.Now, DateTime.Now, "binhncccccccccc");
            return Ok();
        }


    }
}
