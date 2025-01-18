using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VinylShop.Core.Interfaces.Repositories;
using VinylShop.Core.Models;
using VinylShop.DataAccess.Entities;

namespace VinylShop.DataAccess.Repositories;

public class VinylRepository : IVinylRepository
{
    private readonly VinylShopDbContext _context;
    private readonly IMapper _mapper;

    public VinylRepository(VinylShopDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Create(Vinyl vinyl)
    {
        var vinylEntity = new VinylEntity
        {
            Id = vinyl.Id,
            Title = vinyl.Title,
            Artist = vinyl.Artist,
            Genre = vinyl.Genre,
            ReleaseYear = vinyl.ReleaseYear,
            Price = vinyl.Price,
            Stock = vinyl.Stock,
            Description = vinyl.Description,
            IsAvailable = vinyl.IsAvailable,
            Image = vinyl.Image
        };
        await _context.Vinyls.AddAsync(vinylEntity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateImage(Guid vinylId, byte[] imageData)
    {
        var vinylEntity = await _context.Vinyls.SingleOrDefaultAsync(v => v.Id == vinylId);
        if (vinylEntity != null)
        {
            vinylEntity.Image = imageData; // Assuming you have an Image property of type byte[]
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Vinyl>> Get()
    {
        var vinylEntities = await _context.Vinyls
            .AsNoTracking()
            .ToListAsync();
        return _mapper.Map<List<Vinyl>>(vinylEntities);
    }

    public async Task<Vinyl> GetById(Guid id)
    {
        var vinylEntity = await _context.Vinyls
            .AsNoTracking()
            .SingleOrDefaultAsync(v => v.Id == id);
        return _mapper.Map<Vinyl>(vinylEntity);
    }

    public async Task<List<Vinyl>> Search(string searchTerm)
    {
        searchTerm = searchTerm.ToLower();

        var query = _context.Vinyls
            .Where(v =>
                    v.Title.ToLower().Contains(searchTerm) ||
                    v.Artist.ToLower().Contains(searchTerm) ||
                    v.Genre.ToLower().Contains(searchTerm) ||
                    v.Description.ToLower().Contains(searchTerm) ||
                    v.ReleaseYear.ToString().Contains(searchTerm) || // Handle year as string
                    v.Price.ToString().Contains(searchTerm) // Handle price as string
            );

        var vinylEntities = await query.ToListAsync();

        return _mapper.Map<List<Vinyl>>(vinylEntities);
    }

    public async Task<List<Vinyl>> GetFiltered(string? genre, int? decade, string? sortOption, int page, int pageSize)
    {
        var query = _context.Vinyls
            .AsQueryable()
            .Skip((page-1) * pageSize)
            .Take(pageSize);

        if (!string.IsNullOrWhiteSpace(genre))
        {
            genre = genre.ToLower();
            query = query.Where(v => v.Genre.ToLower().Contains(genre.ToLower()));
        }

        if (decade.HasValue)
        {
            int startYear = decade.Value;
            int endYear = startYear + 9;
            query = query.Where(v => v.ReleaseYear >= startYear && v.ReleaseYear <= endYear);
        }

        if (!string.IsNullOrWhiteSpace(sortOption))
        {
            if (sortOption.Contains("Price"))
            {
                query = sortOption.Contains("Low to High")
                    ? query.OrderBy(v => v.Price)
                    : query.OrderByDescending(v => v.Price);
            }
            else if (sortOption.Contains("ReleaseYear"))
            {
                query = sortOption.Contains("LowtoHigh")
                    ? query.OrderBy(v => v.ReleaseYear)
                    : query.OrderByDescending(v => v.ReleaseYear);
            }
            else
            {
                query = query.OrderBy(v => v.Title);
            }
        }
        else
        {
            query = query.OrderBy(v => v.Title);
        }

        var vinylEntities = await query.ToListAsync();
        return _mapper.Map<List<Vinyl>>(vinylEntities);
    }

    public async Task<int> GetTotalVinylCount()
    {
        return await _context.Vinyls.CountAsync();
    }

    public async Task Update(Guid id, string title, string artist, string genre, int releaseYear, decimal price, int stock,
        string description, bool isAvailable)
    {
        await _context.Vinyls
            .Where(v => v.Id == id)
            .ExecuteUpdateAsync(v => v
                .SetProperty(vinyl => vinyl.Title, title)
                .SetProperty(vinyl => vinyl.Artist, artist)
                .SetProperty(vinyl => vinyl.Genre, genre)
                .SetProperty(vinyl => vinyl.ReleaseYear, releaseYear)
                .SetProperty(vinyl => vinyl.Price, price)
                .SetProperty(vinyl => vinyl.Stock, stock)
                .SetProperty(vinyl => vinyl.Description, description)
                .SetProperty(vinyl => vinyl.IsAvailable, isAvailable)
            );
    }

    public async Task Delete(Guid id)
    {
        await _context.Vinyls
            .Where(v => v.Id == id)
            .ExecuteDeleteAsync();
    }
}