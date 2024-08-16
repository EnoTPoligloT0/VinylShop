using CSharpFunctionalExtensions;

namespace VinylShop.Core.Models;

public class Order
{
    private readonly List<OrderItem> _orderItems = [];
    private Order(Guid id, Guid userId, User user, DateTime orderDate, decimal totalAmount)
    {
        Id = id;
        UserId = userId;
        User = user;
        OrderDate = orderDate;
        TotalAmount = totalAmount;
       
    }

    public Guid Id { get; }
    public Guid UserId { get; }

    public User User { get; }
    public DateTime OrderDate { get; }
    public decimal TotalAmount { get; }

    public IReadOnlyList<OrderItem>? OrderItems => _orderItems;
    
    //todo Validation
    public static Result<Order> Create(Guid id, Guid userId, User user, DateTime orderDate, decimal totalAmount)
    {
        var order = new Order(id, userId, user, orderDate, totalAmount);

        return Result.Success(order);
    }
}