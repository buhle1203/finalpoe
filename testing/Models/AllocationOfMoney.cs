using System;
using System.Collections.Generic;

#nullable disable

namespace testing.Models
{
    public partial class AllocationOfMoney
    {
        public Guid AllocationId { get; set; }
        public DateTime DateOfAllocation { get; set; }
        public double AmountAllocated { get; set; }
        public Guid DisasterId { get; set; }

        public virtual ActiveDisaster Disaster { get; set; }
    }
}
