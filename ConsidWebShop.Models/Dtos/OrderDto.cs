using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsidWebShop.Models.Dtos;
public class OrderDto
{
    public int Id { get; set; }
    public DateTime OrderPlacementTime { get; set; }
    public int CustomerId { get; set; }
    public int OrderItemId { get; set; }
}
