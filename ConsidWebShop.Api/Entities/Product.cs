﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsidWebShop.Api.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(500)]
        public string Name { get; set; }
        [MaxLength(5000)]
        public string Description { get; set; }
        [StringLength (1000)]
        public string ImageURL { get; set; }
        [MaxLength(100000)]
        public decimal Price { get; set; }
        [MaxLength (500)]
        public int Qty { get; set; }
        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; }
    }
}
