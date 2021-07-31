using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class ProductCategory
    {
        [Key]
        public byte Id { get; set; }

        [Required]
        [Display(Name = "Product category")]
        [MaxLength(100,ErrorMessage ="Maximum length for product category name is 100 characters")]
        public string Name { get; set; }
    }
}