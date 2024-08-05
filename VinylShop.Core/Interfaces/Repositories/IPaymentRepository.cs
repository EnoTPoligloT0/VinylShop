using VinylShop.Core.Dtos.PaymentDtos;
using VinylShop.Core.Models;

namespace VinylShop.Core.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task Create(Payment payment);
    Task Delete(Guid id);
    Task<List<Payment>> Get();
    Task<Payment> GetById(Guid id);
    Task Update(Guid id, UpdatePaymentRequestDto updatePaymentRequestDto);
}