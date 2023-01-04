using System;
using VirtualMemoryAlgorithms.Algorithms;
using Web.Dto.Solvers;
using Web.Enums.Solvers;

namespace Web.Operations.Solvers
{
    public interface IVirtualMemoryManager
    {
        VirtualMemoryManagerResult Process(VirtualMemoryManagerRequest request);
    }

    public class VirtualMemoryManager : IVirtualMemoryManager
    {        
        private BaseAlgorithm GetAlgorithm(VirtualMemoryManagerRequest request)
        {
            switch (request.Type)
            {
                case VirtualMemoryManagerType.FirstInFirstOut:
                    return new FirstInFirstOut(request.MaxQueue);
                case VirtualMemoryManagerType.LeastFrequentlyUsed:
                    return new LeastFrequentlyUsed(request.MaxQueue);
                case VirtualMemoryManagerType.LeastRecentlyUsed:
                    return new LeastRecentlyUsed(request.MaxQueue);
                case VirtualMemoryManagerType.Optimal:
                    return new Optimal(request.MaxQueue, request.PageReferences);
                case VirtualMemoryManagerType.SecondChance:
                    return new SecondChance(request.MaxQueue);
                default:
                    throw new Exception("Unknown type.");
            }
        }

        public VirtualMemoryManagerResult Process(VirtualMemoryManagerRequest request)
        {
            var processor = this.GetAlgorithm(request);

            foreach (var reference in request.PageReferences)
            {
                processor.AccessPage(reference);
            }

            return new VirtualMemoryManagerResult {
                History = processor.History.GetItems()
            };
        }
    }
}
