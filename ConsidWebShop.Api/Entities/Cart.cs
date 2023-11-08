using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ConsidWebShop.Api.Entities
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //ForeginKey for User
        public int? UserId { get; set; }
        public User? User { get; set; }

        public ICollection<CartItem> CartItem { get; set;} = new List<CartItem>();
    }
}
