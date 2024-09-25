using System.Security.Cryptography.X509Certificates;
using CSharpFunctionalExtensions;

namespace VinylShop.Core.Models;

public class Vinyl
{
    public Guid Id { get; }
    public string Title { get; } = string.Empty;
    public string Artist { get; } = string.Empty;
    public string Genre { get; } = string.Empty;
    public int ReleaseYear { get; }
    public decimal Price { get; }
    public int Stock { get; }
    public string Description { get; } = string.Empty;
    public bool IsAvailable { get; }
    public byte[] Image { get; }
    private Vinyl(Guid id, string title, string artist, string genre, int releaseYear, decimal price,
        int stock, string description, bool isAvailable, byte[] image)
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
        Image = image;

    }

    //todo Validation
    public static Result<Vinyl> Create(Guid vinylId, string title, string artist, string genre,
        int releaseYear, decimal price, int stock, string description, bool isAvailable, string imageBase64)
    {
        if (string.IsNullOrEmpty(title))
            return Result.Failure<Vinyl>("Title cannot be null or empty.");
        if (string.IsNullOrEmpty(artist))
            return Result.Failure<Vinyl>("Artist cannot be null or empty.");
        if (string.IsNullOrEmpty(imageBase64))
            return Result.Failure<Vinyl>("Image cannot be null or empty.");
        if (price <= 0)
            return Result.Failure<Vinyl>("Price must be greater than zero.");
        if (stock < 0)
            return Result.Failure<Vinyl>("Stock cannot be negative.");
        if (releaseYear < 1900 || releaseYear > DateTime.Now.Year)
            return Result.Failure<Vinyl>("Release year must be a valid year.");
        
        byte[] image = Convert.FromBase64String(imageBase64);
        
        var vinyl = new Vinyl(
            vinylId, title, artist, genre, releaseYear, price, stock, description, isAvailable, image);

        return Result.Success(vinyl);
    }
}