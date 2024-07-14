using CSharpFunctionalExtensions;

namespace VinylShop.Core.Models;

public class Shipment
{
    public Guid ShipmentId { get; }
    public Guid OrderId { get; }
    public DateTime ShipmentDate { get; }
    public string TrackingNumber { get; } = string.Empty;
    public string ShipmentStatus { get; } = string.Empty;
    public Order Order { get; }

    private Shipment(Guid shipmentId, Guid orderId, Order order, DateTime shipmentDate,
        string trackingNumber, string shipmentStatus)
    {
        ShipmentId = shipmentId;
        OrderId = orderId;
        Order = order;
        ShipmentDate = shipmentDate;
        TrackingNumber = trackingNumber;
        ShipmentStatus = shipmentStatus;
    }

    //todo Validation
    public static Result<Shipment> Create(Guid shipmentId, Guid orderId, Order order, DateTime shipmentDate,
        string trackingNumber, string shipmentStatus)
    {
        if (string.IsNullOrEmpty(trackingNumber))
        {
            return Result.Failure<Shipment>($"'{nameof(trackingNumber)}' can't be null or empty");
        }

        if (string.IsNullOrEmpty(shipmentStatus))
        {
            return Result.Failure<Shipment>($"'{nameof(shipmentStatus)}' can't be null or empty");
        }


        var shipment = new Shipment(shipmentId, orderId, order, shipmentDate, trackingNumber, shipmentStatus);
        
        return Result.Success(shipment);
    }
}