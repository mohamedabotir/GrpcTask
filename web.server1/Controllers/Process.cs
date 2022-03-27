using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace web.server1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Process : ControllerBase
    {
        public IActionResult RequestProduct(string name){
            return Content("OK "+name);
        }
    }
}