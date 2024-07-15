using System.Security.Cryptography.X509Certificates;
using CSharpFunctionalExtensions;

namespace VinylShop.Core.Models;

public class Vinyl
{
    public const int MAX_TITLE_LENGHT = 250;

    public Guid VinylId { get; }
    public string Title { get; } = string.Empty;
    public string Artist { get; } = string.Empty;
    public string Genre { get; } = string.Empty;
    public int ReleaseYear { get; }
    public decimal Price { get; }
    public int Stock { get; }
    public string Description { get; } = string.Empty;
    public bool IsAvailable { get; }
    
    public OrderItem? OrderItem { get; }

    private Vinyl(Guid vinylId, string title, string artist, string genre, int releaseYear, decimal price,
        int stock, string description, bool isAvailable, OrderItem orderItem)
    {
        VinylId = vinylId;
        Title = title;
        Artist = artist;
        Genre = genre;
        ReleaseYear = releaseYear;
        Price = price;
        Stock = stock;
        Description = description;
        IsAvailable = isAvailable;
        OrderItem = orderItem;
    }

    //todo Validation
    public static Result<Vinyl> Create(Guid vinylId, string title, string artist, string genre,
        int releaseYear, decimal price, int stock, string description, bool isAvailable, OrderItem orderItem)
    {
        var vinyl = new Vinyl(vinylId, title, artist, genre, releaseYear, price, stock, description, isAvailable, orderItem);

        return Result.Success(vinyl);
    }
}