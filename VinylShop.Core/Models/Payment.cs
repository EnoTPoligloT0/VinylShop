namespace VinylShop.Core.Models;

public class Payment
{
    public Guid PaymentId { get; }
    public Guid OrderId { get; }
    public Order Order { get; }
    public DateTime PaymentDate { get; }
    public decimal Amount { get; }
    public string PaymentMethod { get; }

    private Payment(Guid paymentId, Guid orderId, Order order, DateTime paymentDate, decimal amount,
        string paymentMethod)
    {
        PaymentId = paymentId;
        OrderId = orderId;
        Order = order;
        PaymentDate = paymentDate;
        Amount = amount;
        PaymentMethod = paymentMethod;
    }

    //todo Validaiton Result
    public static (Payment Payment, string Error) Create(Guid paymentId, Guid orderId, Order order,
        DateTime paymentDate, decimal amount, string paymentMethod)
    {
        var error = string.Empty;

        if (!string.IsNullOrEmpty(error))
        {
            return (null, error);
        }

        var payment = new Payment(paymentId, orderId, order, paymentDate, amount, paymentMethod);
        return (payment, null);
    }
}