using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsidWebShop.Models.Dtos;
public class OrderItemDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public string ?ProductName { get; set; } 
    public string ?ProductDescription { get; set; } 
    public decimal? Price { get; set; }
    public int? Qty { get; set; }
}
