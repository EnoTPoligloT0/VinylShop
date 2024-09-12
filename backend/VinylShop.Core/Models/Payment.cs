using CSharpFunctionalExtensions;

namespace VinylShop.Core.Models;

public class Payment
{
    private Payment(Guid paymentId, Guid orderId, DateTime paymentDate, decimal amount,
        string paymentMethod)
    {
        PaymentId = paymentId;
        OrderId = orderId;
        PaymentDate = paymentDate;
        Amount = amount;
        PaymentMethod = paymentMethod;
    }

    public Guid PaymentId { get; }
    public Guid OrderId { get; }

    public Order Order { get; }
    public DateTime PaymentDate { get; }
    public decimal Amount { get; }
    public string PaymentMethod { get; } = string.Empty;


    //todo ValidationMethod
    public static Result<Payment> Create(Guid paymentId, Guid orderId,
        DateTime paymentDate, decimal amount, string paymentMethod)
    {
        if (string.IsNullOrEmpty(paymentMethod))
        {
            return Result.Failure<Payment>($"'{nameof(paymentMethod)}' can't be null or empty");
        }

        if (amount <= 0)
        {
            return Result.Failure<Payment>("Amount must be greater than zero");
        }

        var payment = new Payment(paymentId, orderId, paymentDate, amount, paymentMethod);

        return Result.Success(payment);
    }
}