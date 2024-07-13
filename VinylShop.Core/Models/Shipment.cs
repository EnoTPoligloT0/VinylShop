namespace VinylShop.Core.Models;

public class Shipment
{
    public Guid ShipmentId { get; }
    public Guid OrderId { get; }
    public Order Order { get; }
    public DateTime ShipmentDate { get; }
    public string TrackingNumber { get; }
    public string ShipmentStatus { get; }

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
    public static (Shipment Shipment, string Error) Create(Guid shipmentId, Guid orderId,
        Order order, DateTime shipmentDate,
        string trackingNumber, string shipmentStatus)
    {
        var error = string.Empty;

        if (!string.IsNullOrEmpty(error))
        {
            return (null, error);
        }

        var shipment = new Shipment(shipmentId, orderId, order, shipmentDate, trackingNumber, shipmentStatus);
        return (shipment, null);
    }
}