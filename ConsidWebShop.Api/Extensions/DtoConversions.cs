﻿using ConsidWebShop.Api.Entities;
using ConsidWebShop.Models.Dtos;

namespace ConsidWebShop.Api.Extensions;

public static class DtoConversions
{
    public static IEnumerable<ProductDto> ConvertToDto(this IEnumerable<Product> products,
                                                        IEnumerable<ProductCategory> productCategories)
    {
        return (from product in products
                join ProductCategory in productCategories
                on product.CategoryId equals ProductCategory.Id
                select new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    ImageURL = product.ImageURL,
                    Price = product.Price,
                    Qty = product.Qty,
                    CategoryId = product.CategoryId,
                    CategoryName = ProductCategory.Name

                }).ToList();
    }

    public static ProductDto ConvertToDto(this Product product,
                                               ProductCategory productCategory)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            ImageURL = product.ImageURL,
            Price = product.Price,
            Qty = product.Qty,
            CategoryId = product.CategoryId,
            CategoryName = productCategory.Name
        };
    }
    public static ProductToAddDto ConvertToDto(this Product product)
    {
        return new ProductToAddDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            ImageURL = product.ImageURL,
            Price = product.Price,
            Qty = product.Qty,
            CategoryId = product.CategoryId,
        };
    }

    public static IEnumerable<CartItemDto> ConvertToDto(this IEnumerable<CartItem> cartItems,
                                                        IEnumerable<Product> products)
    {
        return (from cartItem in cartItems
                join product in products
                on cartItem.ProductId equals product.Id
                select new CartItemDto
                {
                    Id = cartItem.Id,
                    ProductId = cartItem.ProductId,
                    ProductName = product.Name,
                    ProductDescription = product.Description,
                    ProductImageURL = product.ImageURL,
                    Price = product.Price,
                    CartId = cartItem.CartId,
                    Qty = cartItem.Qty,
                    TotalPrice = product.Price * cartItem.Qty,
                }).ToList();
    }
    public static CartItemDto ConvertToDto(this CartItem cartItem,
                                                       Product product)
    {
        return new CartItemDto
        {
            Id = cartItem.Id,
            ProductId = cartItem.ProductId,
            ProductName = product.Name,
            ProductDescription = product.Description,
            ProductImageURL = product.ImageURL,
            Price = product.Price,
            CartId = cartItem.CartId,
            Qty = cartItem.Qty,
            TotalPrice = product.Price * cartItem.Qty,
        };


    }
    public static IEnumerable<OrderItemDto> ConvertToDto(this IEnumerable<OrderItem> orderItems,
                                                                    IEnumerable<Product> products)
    {
        return (from orderItem in orderItems
                join product in products
                on orderItem.ProductId equals product.Id
                
                select new OrderItemDto
                {
                    Id= orderItem.Id,
                    OrderId= orderItem.OrderId,
                    ProductId= product.Id,
                    Qty = product.Qty
                }).ToList();

    }
    public static OrderDto ConvertToDto(this Order order,
                                        OrderItem orderitems)
    {
        return new OrderDto
        {
            Id = order.Id,
            OrderItemId = orderitems.Id,
            //CustomerId = order.UserId,
            OrderPlacementTime = order.PlacementTime.Date
        };

    }
}
