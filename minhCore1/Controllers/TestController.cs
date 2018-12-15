using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using minhCore1.Models;

namespace minhCore1.Controllers
{
    public class TestController : Controller
    {
        private readonly IConfiguration _config;

        public TestController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            string sendGridApiKey = _config["sendGridApiKey"];

            return View(new TestViewModel { SendGridApiKey = sendGridApiKey });
        }
    }
}