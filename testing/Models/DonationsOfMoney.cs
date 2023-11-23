using System;
using System.Collections.Generic;

#nullable disable

namespace testing.Models
{
    public partial class DonationsOfMoney
    {
        public Guid DonationId { get; set; }
        public DateTime DonatedDate { get; set; }
        public double DonationAmount { get; set; }
        public string DonationDonor { get; set; }
    }
}
