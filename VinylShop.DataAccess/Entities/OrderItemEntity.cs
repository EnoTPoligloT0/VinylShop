using VinylShop.Core.Models;

namespace VinylShop.DataAccess.Entities;

public class OrderItemEntity
{
    public Guid Id { get; set;}
    public Guid OrderId { get;set; }
    public Order Order { get; set; }
    public Guid VinylId { get;set; }
    public int Quantity { get; set;}
    public decimal UnitPrice { get;set; }

    public List<Vinyl> Vinyls { get; set;}
}