using System.Security.Cryptography.X509Certificates;
using CSharpFunctionalExtensions;

namespace VinylShop.Core.Models;

public class Vinyl
{
    public Guid Id { get; }
    public Guid OrderItemId { get; }
    public OrderItem? OrderItem { get; }
    public string Title { get; } = string.Empty;
    public string Artist { get; } = string.Empty;
    public string Genre { get; } = string.Empty;
    public int ReleaseYear { get; }
    public decimal Price { get; }
    public int Stock { get; }
    public string Description { get; } = string.Empty;
    public bool IsAvailable { get; }
    private Vinyl(Guid id, string title, string artist, string genre, int releaseYear, decimal price,
        int stock, string description, bool isAvailable)
    {
        Id = id;
        Title = title;
        Artist = artist;
        Genre = genre;
        ReleaseYear = releaseYear;
        Price = price;
        Stock = stock;
        Description = description;
        IsAvailable = isAvailable;
     
    }

    //todo Validation
    public static Result<Vinyl> Create(Guid vinylId, string title, string artist, string genre,
        int releaseYear, decimal price, int stock, string description, bool isAvailable)
    {
        var vinyl = new Vinyl(
            vinylId, title, artist, genre, releaseYear, price, stock, description, isAvailable);

        return Result.Success(vinyl);
    }
}