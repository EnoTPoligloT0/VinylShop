namespace VinylShop.Core.Models;

public class Order
{
    public Guid Id { get; }
    public Guid CustomerId { get; }
    public DateTime OrderDate { get; }
    public decimal TotalAmount { get; }
    public List<OrderItem> OrderItems { get; }

    private Order(Guid id, Guid customerId, DateTime orderDate, decimal totalAmount, List<OrderItem> orderItems)
    {
        Id = id;
        CustomerId = customerId;
        OrderDate = orderDate;
        TotalAmount = totalAmount;
        OrderItems = orderItems;
    }

    public static (Order Order, string Error) Create(Guid id, Guid customerId, DateTime orderDate, decimal totalAmount,
        List<OrderItem> orderItems)
    {
        var error = string.Empty;

        if (!string.IsNullOrEmpty(error))
        {
            return (null, error);
        }

        var order = new Order(id, customerId, orderDate, totalAmount, orderItems);
        return (order, null);
    }
}