using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsidWebShop.Api.Entities
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CartId { get; set; }
        public Cart cart { get; set; } = new Cart();
        public int ProductId { get; set; }
        public Product product { get; set; } = new Product();
        [MaxLength(200)]
        public int Qty { get; set; }
    }
}
