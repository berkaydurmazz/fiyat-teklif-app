using System.ComponentModel.DataAnnotations;

namespace webapi.ViewModel.Customer
{
    public class CustomerCreateVM
    {
        public int Id { get; set; }
        [Required]
        public string Adi { get; set; }
        [Required]
        public string Soyadi { get; set; }
        [Required]
        public string TelefonNumarasi { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
