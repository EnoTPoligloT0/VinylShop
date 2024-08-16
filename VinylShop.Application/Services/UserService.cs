using VinylShop.Application.Auth;
using VinylShop.Core.Interfaces.Repositories;
using VinylShop.Core.Interfaces.Services;
using VinylShop.Core.Models;
using VinylShop.Infrastructure;

namespace VinylShop.Application.Services;

public class UserService : IUserService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    
    public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }


    public async Task<string> Login(string email, string password)
    {
        var user = await _userRepository.GetByEmail(email) ?? throw new Exception("User not found.");

        var result = _passwordHasher.Verify(password, user.PasswordHash);

        if (result == false)
        {
            throw new Exception("Failed to login");
        }

        var token = _jwtProvider.Generate(user);

        return token;
        
    }

    

    public async Task Register(Guid userId, string passwordHash, string email)
    {
        var hashedPassword = _passwordHasher.Generate(passwordHash);

        var userResult = User.CreateForRegistration(userId, hashedPassword, email);

        if (userResult.IsSuccess)
        {
            var user = userResult.Value;

            await _userRepository.Add(user);
        }
        else
        {
            throw new Exception($"User creation failed:{userResult.Error}");
        }
    }
}