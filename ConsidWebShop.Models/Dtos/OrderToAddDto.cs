using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsidWebShop.Models.Dtos;

public class OrderToAddDto
{
    
    public int Id { get; set; }
    public DateTime PlacementTime { get; set; }

    //ForeignKey for Customer
    public int UserId { get; set; }

    public int OrderItemId { get; set; }
}
