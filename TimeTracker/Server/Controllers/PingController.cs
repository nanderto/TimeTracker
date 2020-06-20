using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        // GET: api/<PingController>
        [HttpGet]
        public OK Get()
        {
            return new TimeTracker.Shared.OK() { OKProperty = "OK" };
        }

    }
}
