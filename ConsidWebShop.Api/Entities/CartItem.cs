using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsidWebShop.Api.Entities;

public class CartItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int CartId { get; set; }
    public Cart Cart { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
    [MaxLength(200)]
    public int Qty { get; set; }
}
