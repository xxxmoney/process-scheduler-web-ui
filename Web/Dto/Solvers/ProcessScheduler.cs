using System.Collections.Generic;
using Web.Enums.Solvers;

namespace Web.Dto.Solvers
{
    public class ProcessSchedulerRequest
    {
        public ProcessSchedulerType Type { get; set; }
        public bool? Preemptive { get; set; }
        public int? TimeSlice { get; set; }
        public IEnumerable<ProcessSchedulerProcess> Processes { get; set; }
    }

    public class ProcessSchedulerResult
    {
        public IEnumerable<ProcessSchedulerHistoryItem> History { get; set; }
    }

    public class ProcessSchedulerHistoryItem
    {
        public int ProcessId { get; set; }
        public int Time { get; set; }
    }

    public class ProcessSchedulerProcess
    {
        public int ProcessId { get; set; }
        public int ArrivalTime { get; set; }
        public int BurstTime { get; set; }
        public int? Priority { get; set; }
        public ProcessSchedulerProcessInterruption Interruption { get; set; }
    }

    public class ProcessSchedulerProcessInterruption
    {
        public int Limit { get; set; }
        public int WaitTime { get; set; }
    }
}
