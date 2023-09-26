namespace webapi.Entity
{
    public class Product : BaseEntity
    {
        public string productName { get; set; }
        public string productDescp { get; set; }
        public string productSize { get; set; }
        public double price { get; set}
        public string suppliers { get; set; }
        public double kdvRate { get; set; }
        public string category { get; set; }
    }
}