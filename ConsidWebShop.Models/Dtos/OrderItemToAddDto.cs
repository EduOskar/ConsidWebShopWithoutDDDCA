﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsidWebShop.Models.Dtos;
public class OrderItemToAddDto
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Qty { get; set; }
}
