using CSharpFunctionalExtensions;
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

    public async Task<Result> Register(string email, string password)
    {
        var existingUser = await _userRepository.GetByEmail(email);
        if (existingUser != null)
        {
            return Result.Failure("A user with this email already exists.");
        }

        var hashedPassword = _passwordHasher.Generate(password);

        var userResult = User.CreateForRegistration(
            new Guid(),
            hashedPassword,
            email);

        if (userResult.IsSuccess)
        {
            var user = userResult.Value;

            await _userRepository.Add(user);

            return Result.Success();
        }
        else
        {
            return Result.Failure($"User creation failed: {userResult.Error}");
        }
    }

    public async Task<List<User>> GetUsers()
    {
        return await _userRepository.Get();
    }

    public async Task<User> GetUserById(Guid id)
    {
        return await _userRepository.GetById(id);
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _userRepository.GetByEmail(email);
    }
}