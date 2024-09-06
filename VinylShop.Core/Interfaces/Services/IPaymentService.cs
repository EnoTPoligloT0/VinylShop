using VinylShop.Core.Models;

namespace VinylShop.Core.Interfaces.Services;

public interface IPaymentService
{
    Task CreatePayment(Payment payment);
    Task DeletePayment(Guid id);
    Task<List<Payment>> GetPayments();
    Task<Payment> GetPaymentById(Guid id);
    Task<Payment> GetPaymentByOrderId(Guid orderId);
    Task UpdatePayment(Guid id, decimal amount, string paymentMethod);
}