using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LvDao_Tourism_Info_Management_System.Controllers
{
    public class HelloWorldController:Controller
    {
        public IActionResult Index()
        {
            return Json(new { name = "helloworld" });
        }
    }
}
