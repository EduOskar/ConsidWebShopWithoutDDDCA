using ConsidWebShop.Api.Entities;
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
    public static Product ConvertFromDto(ProductDto productDto)
    {

        return new Product
        {
            Id = productDto.Id,
            Name = productDto.Name,
            Description = productDto.Description,
            ImageURL = productDto.ImageURL,
            Price = productDto.Price,
            Qty = productDto.Qty,
            CategoryId = productDto.CategoryId,

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
                    Id = orderItem.Id,
                    OrderId = orderItem.OrderId,
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductDescription = product.Description,
                }).ToList();

    }
    public static OrderItemDto ConvertToDto(this OrderItem orderItems,
                                                      Product product)
    {

        return new OrderItemDto
        {
            Id = orderItems.Id,
            OrderId = orderItems.Id,
            ProductId = orderItems.ProductId,
            ProductName = product?.Name,
            Price = product?.Price,
            ProductDescription = product?.Description,

        };
    }
    public static IEnumerable<OrderDto> ConvertToDto(this IEnumerable<Order> orders,
                                             IEnumerable<OrderItem> orderItems)
    {

        return orders.Select(order => new OrderDto
        {
            OrderItemsId = order.OrderItemId,
            UserId = order.UserId,
            OrderPlacementTime = order.PlacementTime,
            OrderItems = orderItems
            .Where(oi => oi.OrderId == order.Id)
            .Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                OrderId = oi.OrderId,
                ProductId = oi.ProductId,
                ProductName = oi.Product.Name,
                Price = oi.Product.Price,
                ProductDescription = oi.Product.Description,
            })
            .ToList()
        }).ToList();
    }
    public static OrderDto ConvertToDto(this Order orders,
                                           IEnumerable<OrderItem> orderItems)
    {
        return new OrderDto
        {
            OrderItemsId = orders.OrderItemId,
             UserId = orders.UserId,
            OrderPlacementTime = orders.PlacementTime,
            OrderItems = orderItems
              .Where(oi => oi.OrderId == orders.Id)
              .Select(oi => new OrderItemDto
              {
                  Id = oi.Id,
                  OrderId = oi.OrderId,
                  ProductId = oi.ProductId,
                  ProductName = oi.Product.Name,
                  Price = oi.Product.Price,
                  ProductDescription = oi.Product.Description,
                  Qty = oi.Product.Qty,
              })
              .ToList()
        };
    }
    public static OrderDto ConvertToDto(this Order order)
    {
        return new OrderDto
        {
            OrderItemsId = order.OrderItems.FirstOrDefault()?.Id ?? 0, // Set appropriately
            UserId = order.UserId,
            OrderPlacementTime = order.PlacementTime,
            OrderItems = order.OrderItems.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                OrderId = oi.OrderId,
                ProductId = oi.ProductId,
                ProductName = oi.Product.Name,
                Price = oi.Product.Price,
                ProductDescription = oi.Product.Description,
            }).ToList()
        };
    }
    public static OrderToAddDto ConvertToDo(this Order order)
    {
        return new OrderToAddDto
        {
            OrderItemId = order.OrderItemId,
            PlacementTime = order.PlacementTime,
            UserId = order.UserId         
        };
    }

}
