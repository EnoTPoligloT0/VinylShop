using System.Security.Cryptography.X509Certificates;

namespace VinylShop.Core.Models;

public class Vinyl
{
    public const int MAX_TITLE_LENGHT = 250;

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

 

    public Guid Id { get; }
    public string Title { get; } = string.Empty;
    public string Artist { get; } = string.Empty;
    public string Genre { get; } = string.Empty;
    public int ReleaseYear { get; }
    public decimal Price { get; }
    public int Stock { get; }
    public string Description { get; } = string.Empty;
    public bool IsAvailable { get; }

    public static (Vinyl Vinyl, string Erorr) Create(Guid id, string title, string artist, string genre,
        int releaseYear, decimal price, int stock, string description, bool isAvailable)
    {
        var error = string.Empty;

        if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGHT)
        {
            error = "Title can not be empty or longer then 250 symbols";
        }

        var vinyl = new Vinyl(id, title, artist, genre, releaseYear, price, stock, description, isAvailable);

        return (vinyl, error);


    }
    //public ICollection<OrderItem> OrderItems { get; set; }
}