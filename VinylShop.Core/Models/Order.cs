namespace VinylShop.Core.Models;

public class Order
{
    public Guid Id { get; }
    public Guid UserId { get; }
    public DateTime OrderDate { get; }
    public decimal TotalAmount { get; }
    
    public User User { get; }
    public List<OrderItem> OrderItems { get; }

    private Order(Guid id, Guid userId, DateTime orderDate, decimal totalAmount, List<OrderItem> orderItems)
    {
        Id = id;
        UserId = userId;
        OrderDate = orderDate;
        TotalAmount = totalAmount;
        OrderItems = orderItems;
    }

    //todo Validation
    public static (Order Order, string Error) Create(Guid id, Guid userId, DateTime orderDate, decimal totalAmount,
        List<OrderItem> orderItems)
    {
        var error = string.Empty;

        if (!string.IsNullOrEmpty(error))
        {
            return (null, error);
        }

        var order = new Order(id, userId, orderDate, totalAmount, orderItems);
        return (order, null);
    }
}