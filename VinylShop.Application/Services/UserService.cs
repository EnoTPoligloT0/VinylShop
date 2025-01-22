using CSharpFunctionalExtensions;
using VinylShop.Application.Auth;
using VinylShop.Core.Interfaces.Repositories;
using VinylShop.Core.Interfaces.Services;
using VinylShop.Core.Models;
using Newtonsoft.Json;

namespace VinylShop.Application.Services;

public class UserService : IUserService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly HttpClient _httpClient;

    public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtProvider jwtProvider, HttpClient httpClient)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _httpClient = httpClient;
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
    public async Task<string> LoginWithGoogle(string googleToken)
    {
        var userInfo = await GetGoogleUserInfo(googleToken);
        if (userInfo == null)
        {
            throw new Exception("Invalid Google token.");
        }

        var user = await _userRepository.GetByEmail(userInfo.Email);
        if (user == null)
        {
            var uniqueHash = Guid.NewGuid().ToString();
            var userResult = User.CreateForRegistration(
                Guid.NewGuid(),
                uniqueHash,
                userInfo.Email);

            if (userResult.IsFailure)
            {
                throw new Exception($"User creation failed: {userResult.Error}");
            }

            user = userResult.Value;
            await _userRepository.Add(user);
        }

        return _jwtProvider.Generate(user);
    }

    private async Task<GoogleUserInfo> GetGoogleUserInfo(string token)
    {
        var response = await _httpClient.GetStringAsync($"https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={token}");
        return JsonConvert.DeserializeObject<GoogleUserInfo>(response);
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
