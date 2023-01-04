using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Dto.Solvers;
using Web.Operations.Solvers;

namespace Web.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SolverController : ControllerBase
    {
        private readonly IVirtualMemoryManager virtualMemoryManager;
        private readonly IProcessScheduler processScheduler;

        public SolverController(IProcessScheduler processScheduler, IVirtualMemoryManager virtualMemoryManager)
        {
            this.processScheduler = processScheduler;
            this.virtualMemoryManager = virtualMemoryManager;
        }

        [HttpPost(nameof(RunProcessScheduler))]
        public IActionResult RunProcessScheduler([FromBody] ProcessSchedulerRequest request)
        {
            var result = processScheduler.Process(request);
            return Ok(result);
        }

        [HttpPost(nameof(RunVirtualManager))]
        public IActionResult RunVirtualManager([FromBody] VirtualMemoryManagerRequest request)
        {
            var result = this.virtualMemoryManager.Process(request);
            return Ok(result);
        }

    }
}
