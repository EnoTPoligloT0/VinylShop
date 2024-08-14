using VinylShop.Core.Interfaces.Repositories;
using VinylShop.Core.Interfaces.Services;
using VinylShop.Core.Models;

namespace VinylShop.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task CreatePayment(Payment payment)
    {
        await _paymentRepository.Create(payment);
    }


    public async Task<List<Payment>> GetPayments()
    {
        return await _paymentRepository.Get();
    }

    public async Task<Payment> GetPaymentById(Guid id)
    {
        return await _paymentRepository.GetById(id);
    }

    public async Task UpdatePayment(Guid id, decimal amount, string paymentMethod)
    {
        await _paymentRepository.Update(id, amount, paymentMethod);
    }
    
    public async Task DeletePayment(Guid id)
    {
        await _paymentRepository.Delete(id);
    }
}
