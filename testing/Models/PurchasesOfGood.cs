using System;
using System.Collections.Generic;

#nullable disable

namespace testing.Models
{
    public partial class PurchasesOfGood
    {
        public Guid PurchaseId { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public double AmountRequired { get; set; }
        public int NumberOfItemsPurchased { get; set; }
        public Guid DonationOfGoodsCategoryId { get; set; }
        public Guid DisasterId { get; set; }

        public virtual ActiveDisaster Disaster { get; set; }
        public virtual DonationOfGoodsCategory DonationOfGoodsCategory { get; set; }
    }
}
