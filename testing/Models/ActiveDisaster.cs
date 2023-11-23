using System;
using System.Collections.Generic;

#nullable disable

namespace testing.Models
{
    public partial class ActiveDisaster
    {
        public ActiveDisaster()
        {
            AllocationOfGoods = new HashSet<AllocationOfGood>();
            AllocationOfMoneys = new HashSet<AllocationOfMoney>();
            PurchasesOfGoods = new HashSet<PurchasesOfGood>();
        }

        public Guid DisasterId { get; set; }
        public string DisasterLocation { get; set; }
        public string DisasterDescription { get; set; }
        public DateTime DisasterStartDate { get; set; }
        public DateTime DisasterEndDate { get; set; }
        public string DisasterAidTypeWanted { get; set; }

        public virtual ICollection<AllocationOfGood> AllocationOfGoods { get; set; }
        public virtual ICollection<AllocationOfMoney> AllocationOfMoneys { get; set; }
        public virtual ICollection<PurchasesOfGood> PurchasesOfGoods { get; set; }
    }
}
