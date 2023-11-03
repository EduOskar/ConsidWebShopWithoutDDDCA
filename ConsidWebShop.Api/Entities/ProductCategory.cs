using System.ComponentModel.DataAnnotations;

namespace ConsidWebShop.Api.Entities
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
