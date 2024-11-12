using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VinylShop.Core.Enums;
using VinylShop.Core.Interfaces.Repositories;
using VinylShop.Core.Models;
using VinylShop.DataAccess.Entities;

namespace VinylShop.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly VinylShopDbContext _context;
    private readonly IMapper _mapper;

    public UserRepository(VinylShopDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task Add(User user)
    {
        var roleEntity = await _context.Roles
                             .SingleOrDefaultAsync(r => r.Id == (int)Role.User)
                         ?? throw new InvalidOperationException();
        
        var userEntity = new UserEntity
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PasswordHash = user.PasswordHash,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            AddressLine1 = user.AddressLine1,
            AddressLine2 = user.AddressLine2,
            City = user.City,
            State = user.State,
            ZipCode = user.ZipCode,
            Roles = [roleEntity]
        };
        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
    }


    public async Task<List<User>> Get()
    {
        var userEntities = await _context.Users
            .AsNoTracking()
            .Include(c => c.Orders)
            .ToListAsync();
        
        return _mapper.Map<List<User>>(userEntities);
    }

    public async Task<User> GetById(Guid id)
    {
        var userEntity = await _context.Users
            .AsNoTracking()
            .Include(c => c.Orders)
            .SingleOrDefaultAsync(u => u.UserId == id);
        return _mapper.Map<User>(userEntity);
    }

    public async Task<User?> GetByEmail(string email)
    {
        var userEntity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);
        return _mapper.Map<User?>(userEntity);
    }
    
    public async Task Update(Guid id, string firstName, string lastName, string? phoneNumber, string? addressLine1,
        string? addressLine2,
        string? city, string? state, string? zipCode)
    {
        await _context.Users
            .Where(c => c.UserId == id)
            .ExecuteUpdateAsync(
                u => u
                    .SetProperty(user => user.FirstName, firstName)
                    .SetProperty(user => user.LastName, lastName)
                    .SetProperty(user => user.PhoneNumber, phoneNumber)
                    .SetProperty(user => user.AddressLine1, addressLine1)
                    .SetProperty(user => user.AddressLine2, addressLine2)
                    .SetProperty(user => user.City, city)
                    .SetProperty(user => user.State, state)
                    .SetProperty(user => user.ZipCode, zipCode));
    }

    public Task UpdatePassword(string password)
    {
        throw new NotImplementedException();
    }
    public async Task<HashSet<Permission>> GetUserPermissions(Guid userId)
    {
        var roles = await _context.Users
            .AsNoTracking()
            .Include(u => u.Roles)
            .ThenInclude(r => r.Permissions)
            .Where(u => u.UserId == userId)
            .Select(u => u.Roles)
            .ToArrayAsync();

        return roles
            .SelectMany(r => r)
            .SelectMany(r => r.Permissions)
            .Select(p => (Permission)p.Id)
            .ToHashSet();
    }


    public async Task Delete(Guid id)
    {
        await _context.Users
            .Where(u => u.UserId == id)
            .ExecuteDeleteAsync();
    }
}