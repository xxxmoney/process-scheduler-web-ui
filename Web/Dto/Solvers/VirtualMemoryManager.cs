using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VirtualMemoryAlgorithms.Data;
using Web.Enums.Solvers;

namespace Web.Dto.Solvers
{
    public class VirtualMemoryManagerRequest
    {
        [Required]
        public VirtualMemoryManagerType Type { get; set; }
        [Required]
        public int MaxQueue { get; set; }
        [Required]
        public IEnumerable<int> PageReferences { get; set; }
    }

    public class VirtualMemoryManagerResult
    {
        public HistoryItem[][] History { get; set; }
    }
}
