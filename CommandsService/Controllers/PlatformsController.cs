using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [ApiController]
    [Route("api/c/[controller]")]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {

        }
        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            System.Console.WriteLine("=> InBound POST # Command Service <=");
            return Ok("Inbound Test OK from Platforms Controller");
        }
    }
}