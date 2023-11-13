using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsidWebShop.Models.Dtos;
public class OrderDto
{
    public DateTime OrderPlacementTime { get; set; }
    public int UserId { get; set; }
    public int OrderItemsId { get; set; }
    public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
}
