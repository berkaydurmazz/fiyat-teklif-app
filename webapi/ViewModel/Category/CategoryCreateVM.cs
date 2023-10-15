using System.ComponentModel.DataAnnotations;

namespace webapi.ViewModel.Category
{
    public class CategoryCreateVM
    {
        public int Id { get; set; }
        [Required]]
        public string catName { get; set; }
    }
}