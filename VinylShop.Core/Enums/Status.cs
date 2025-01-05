namespace VinylShop.Core.Enums;

public enum Status
{
    //Pre-paid
    Pending,
    Confirmed,
    Paid,

    //Processing
    Processing,
    Shipped,
    Transit,

    //Delivery
    Delivered,

    //Issue
    Canceled,
    Refunded,      
    Failed,
    Lost
}