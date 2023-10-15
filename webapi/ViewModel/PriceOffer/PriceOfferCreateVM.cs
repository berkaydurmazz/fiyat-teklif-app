using System.ComponentModel.DataAnnotations;

namespace webapi.ViewModel.PriceOffer
{
    public class PriceOfferCreateVM
    {
        public int OfferId { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public string EndDate { get; set; }
        [Required]
        public Customer customer { get; set; }
        [Required]
        public Product product { get; set; }
        [Required]
        public int number { get; set; }
        [Required]
        public double price { get; set; }
        [Required]
        public double discountRate { get; set; }
        [Required]
        public double totalPrice { get; set; }
    }
}