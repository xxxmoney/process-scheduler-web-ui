using ProcessScheduling.Core.Data;
using ProcessScheduling.Core.Schedulers;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Dto.Solvers;

namespace Web.Operations.Solvers
{
    public interface IProcessScheduler
    {
        ProcessSchedulerResult Process(ProcessSchedulerRequest request);
    }

    public class ProcessScheduler : IProcessScheduler
    {
        private List<Process> Map(ProcessSchedulerRequest request)
        {            
            return request.Processes.Select(process =>
            {
                var result = process.Priority.HasValue ? 
                    new PriorityProcess(process.ProcessId, process.ArrivalTime, process.BurstTime, process.Priority.Value) :
                    new Process(process.ProcessId, process.ArrivalTime, process.BurstTime);

                if (process.Interruption != null)
                {
                    result.Interruption = new Interruption(process.Interruption.Limit, process.Interruption.WaitTime);
                }

                return result;
            }).ToList();
        }

        private Scheduler GetScheduler(ProcessSchedulerRequest request)
        {
            var processes = Map(request);

            switch (request.Type)
            {
                case Enums.Solvers.ProcessSchedulerType.FirstComeFirstServe:
                    return new FirstComeFirstServeScheduler(processes);
                case Enums.Solvers.ProcessSchedulerType.Priority:
                    return new PriorityScheduler(request.Preemptive.Value, processes);
                case Enums.Solvers.ProcessSchedulerType.RoundRobin:
                    return new RoundRobinScheduler(processes, request.TimeSlice.Value);
                case Enums.Solvers.ProcessSchedulerType.ShortestJobFirst:
                    return new ShortestJobFirstScheduler(processes);
                case Enums.Solvers.ProcessSchedulerType.ShortestRemainingTimeFirst:
                    return new ShortestRemainingTimeFirstScheduler(processes);
                default:
                    throw new Exception("Unknown type.");
            }
        }

        public ProcessSchedulerResult Process(ProcessSchedulerRequest request)
        {
            var scheduler = GetScheduler(request);

            scheduler.Process();

            return new ProcessSchedulerResult
            {
                History = scheduler.GetHistory().items.Select(item => new ProcessSchedulerHistoryItem
                {
                    ProcessId = item.Process.Id,
                    Time = item.Time
                }).ToList(),
            };
        }
    }
}
