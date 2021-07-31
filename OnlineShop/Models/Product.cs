using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class Product
    {
        public Product()
        {

        }

        public Product(Product product)
            :base()
        {
            this.Id = product.Id;
            this.Name = product.Name;
            this.Price = product.Price;
            this.Ammount = product.Ammount;
            this.Description = product.Description;
            this.ProductCategoryId = product.ProductCategoryId;
            this.Image = product.Image;
        }

        public double TotalProductPrice
        {
            get
            {
                return Price * (double)Ammount;
            }
        }

    //    [Key]
    //    [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public byte Id { get; set; }

        [Required]
        [Display(Name = "Product name")]
        [MaxLength(100, ErrorMessage = "Maximum length for product name is 100 characters")]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [RegularExpression(@"^\d*\.?\d{0,2}$", ErrorMessage = "Price can't have more than 2 decimal places")]
        public double Price { get; set; }

        public decimal Ammount { get; set; }

        [Required]
        [Display(Name = "Measurement unit")]
        public string UnitOfMeasurement { get; set; }

        [MaxLength(250, ErrorMessage = "Maximum length for product descitption is 250 characters")]
        public string Description { get; set; }

        public ProductCategory ProductCategory { get; set; }

        [Required]
        [Display(Name = "Product category")]
        public byte ProductCategoryId { get; set; }

        public byte[] Image { get; set; }

    }
}