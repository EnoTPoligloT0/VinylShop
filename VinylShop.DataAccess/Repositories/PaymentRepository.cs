using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VinylShop.Core.Interfaces.Repositories;
using VinylShop.Core.Models;
using VinylShop.DataAccess.Entities;

namespace VinylShop.DataAccess.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly VinylShopDbContext _context;
    private readonly IMapper _mapper;

    public PaymentRepository(VinylShopDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task Create(Payment payment)
    {
        var paymentEntity = new PaymentEntity
        {
            PaymentId = payment.PaymentId,
            OrderId = payment.OrderId,
            PaymentDate = payment.PaymentDate,
            Amount = payment.Amount,
            PaymentMethod = payment.PaymentMethod,
            StripePaymentId = payment.StripePaymentId
        };
        await _context.Payment.AddAsync(paymentEntity);
        await _context.SaveChangesAsync();
    }



    public async Task<List<Payment>> Get()
    {
        var paymentEntities = await _context.Payment
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<List<Payment>>(paymentEntities);
    }

    public async Task<Payment> GetById(Guid id)
    {
        var paymentEntity = await _context.Payment
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.PaymentId == id);
        
        return _mapper.Map<Payment>(paymentEntity);
    }

    public async Task<Payment> GetByOrderId(Guid orderId)
    {
        var paymentEntity = await _context.Payment
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.OrderId == orderId);
        
        return _mapper.Map<Payment>(paymentEntity);
    }

    public async Task Update(Guid id, decimal amount, string paymentMethod)
    {
        await _context.Payment
            .Where(c => c.PaymentId == id)
            .ExecuteUpdateAsync(
                s => s
                    .SetProperty(c => c.Amount, amount)
                    .SetProperty(c => c.PaymentMethod, paymentMethod)
            );
    }

    public async Task Delete(Guid id)
    {
        await _context.Payment
            .Where(c => c.PaymentId == id)
            .ExecuteDeleteAsync();
    }
 
}
