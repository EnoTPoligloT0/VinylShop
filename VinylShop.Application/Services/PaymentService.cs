using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using Stripe;
using VinylShop.Core.Interfaces.Repositories;
using VinylShop.Core.Interfaces.Services;
using VinylShop.Core.Models;

namespace VinylShop.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly string _stripeSecretKey;
    private readonly string _stripePublicKey;

    public PaymentService(IPaymentRepository paymentRepository, IConfiguration configuration)
    {
        _paymentRepository = paymentRepository;
        _stripeSecretKey = configuration["Stripe:SecretKey"];
        _stripePublicKey = configuration["Stripe:PublicKey"];

        StripeConfiguration.ApiKey = _stripeSecretKey;
    }

    public async Task CreatePayment(Payment payment)
    {
        await _paymentRepository.Create(payment);
    }

    public async Task<Result<Payment>> ProcessStripePayment(Guid orderId, decimal amount, string paymentMethod, string currency = "usd")
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = Convert.ToInt64(amount * 100),
            Currency = currency,
            PaymentMethod = paymentMethod,
            Confirm = true
        };

        var service = new PaymentIntentService();
        var intent = await service.CreateAsync(options);

        var paymentResult = Payment.Create(Guid.NewGuid(), orderId, DateTime.UtcNow, amount, paymentMethod, intent.Id);

        if (!paymentResult.IsSuccess)
            return Result.Failure<Payment>(paymentResult.Error);

        await _paymentRepository.Create(paymentResult.Value);

        return paymentResult;
    }


    public async Task<List<Payment>> GetPayments()
    {
        return await _paymentRepository.Get();
    }

    public async Task<Payment> GetPaymentById(Guid id)
    {
        return await _paymentRepository.GetById(id);
    }

    public async Task<Payment> GetPaymentByOrderId(Guid orderId)
    {
        return await _paymentRepository.GetByOrderId(orderId);
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
