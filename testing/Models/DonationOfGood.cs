using System;
using System.Collections.Generic;

#nullable disable

namespace testing.Models
{
    public partial class DonationOfGood
    {
        public Guid DonationId { get; set; }
        public DateTime DonationDate { get; set; }
        public int DonationNumberOfItems { get; set; }
        public Guid CategoryId { get; set; }
        public string DonationDescription { get; set; }
        public string DonationDonor { get; set; }

        public virtual DonationOfGoodsCategory Category { get; set; }
    }
}
