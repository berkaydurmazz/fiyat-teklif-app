namespace webapi.ViewModel.PriceOffer
{
    public class PriceOfferGridVM
    {
        public int OfferId { get; set; }
        public string Date { get; set; }
        public string EndDate { get; set; }
        public Customer customer{ get; set; }
        public Product product { get; set; }
        public int number { get; set; }
        public double price { get; set; }
        public double discountRate  { get; set; }
        public double totalPrice { get; set; }
    }
}
