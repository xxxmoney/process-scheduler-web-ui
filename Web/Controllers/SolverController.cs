using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Operations.Solvers;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SolverController : ControllerBase
    {
        private readonly IVirtualMemoryManager virtualMemoryManager;
        private readonly IProcessScheduler processScheduler;


        [HttpPost(nameof(RunProcessScheduler))]
        public IActionResult RunProcessScheduler()
        {
            return Ok();   
        }

        [HttpPost(nameof(RunVirtualManager))]
        public IActionResult RunVirtualManager()
        {
            return Ok();
        }

    }
}
