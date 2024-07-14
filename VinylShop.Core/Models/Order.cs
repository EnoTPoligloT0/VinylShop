using CSharpFunctionalExtensions;

namespace VinylShop.Core.Models;

public class Order
{
    public Guid Id { get; }
    public Guid UserId { get; }

    public User User { get; }
    public DateTime OrderDate { get; }
    public decimal TotalAmount { get; }

    public List<OrderItem> OrderItems { get; }

    private Order(Guid id, Guid userId, User user, DateTime orderDate, decimal totalAmount, List<OrderItem> orderItems)
    {
        Id = id;
        UserId = userId;
        User = user;
        OrderDate = orderDate;
        TotalAmount = totalAmount;
        OrderItems = orderItems;
    }

    //todo Validation
    public static Result<Order> Create(Guid id, Guid userId, User user, DateTime orderDate, decimal totalAmount,
        List<OrderItem> orderItems)
    {
        var order = new Order(id, userId, user, orderDate, totalAmount, orderItems);

        return Result.Success(order);
    }
}