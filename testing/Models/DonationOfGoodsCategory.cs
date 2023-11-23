using System;
using System.Collections.Generic;

#nullable disable

namespace testing.Models
{
    public partial class DonationOfGoodsCategory
    {
        public DonationOfGoodsCategory()
        {
            AllocationOfGoods = new HashSet<AllocationOfGood>();
            DonationOfGoods = new HashSet<DonationOfGood>();
            PurchasesOfGoods = new HashSet<PurchasesOfGood>();
        }

        public Guid DonationCategoryId { get; set; }
        public string DonationCategoryName { get; set; }

        public virtual ICollection<AllocationOfGood> AllocationOfGoods { get; set; }
        public virtual ICollection<DonationOfGood> DonationOfGoods { get; set; }
        public virtual ICollection<PurchasesOfGood> PurchasesOfGoods { get; set; }
    }
}
