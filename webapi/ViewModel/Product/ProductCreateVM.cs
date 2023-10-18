using System.ComponentModel.DataAnnotations;

namespace webapi.ViewModel.Product
{
    public class ProductCreateVM
    {
        public int Id { get; set; }
        [Required]
        public string productName { get; set; }
        [Required]
        public string productDescp { get; set; }
        [Required]
        public string productSize { get; set; }
        [Required]
        public double price { get; set; }
        [Required]
        public string suppliers { get; set; }
        [Required]
        public double kdvRate { get; set; }
        [Required]
        public string category { get; set; }

    }
}