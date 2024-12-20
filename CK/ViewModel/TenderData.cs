using Humanizer;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace CK.ViewModel
{
    public class TenderData : ConnectionDB
    {
        public string? BranchName { get; set; }
        public string? Coupon { get; set; }
        public double Price { get; set; }
        public string? OfferId { get; set; }
        public string? TypePriceId { get; set; }
        public DateTime? ActivationDateTime { get; set; }
        public double NetToMerchant { get; set; }
        public double Vat { get; set; }
        public double WaffarhaCommission { get; set; }
        public string? ActivatedBy { get; set; }
    }
}
