using CSharpFunctionalExtensions;

namespace VinylShop.Core.Models;

public class User
{
    public Guid UserId { get; }
    public string? FirstName { get; } = string.Empty;
    public string? LastName { get; } = string.Empty;
    public string PasswordHash { get; } = string.Empty;
    public string Email { get; } = string.Empty;
    public string? PhoneNumber { get; } = string.Empty;
    public string? AddressLine1 { get; } = string.Empty;
    public string? AddressLine2 { get; } = string.Empty;
    public string? City { get; } = string.Empty;
    public string? State { get; } = string.Empty;
    public string? ZipCode { get; } = string.Empty;
    public List<Order>? Orders { get; }
    
    private User(Guid userId, string passwordHash, string email)
    {
        UserId = userId;
        PasswordHash = passwordHash;
        Email = email;
        Orders = new List<Order>();
    }

    private User(Guid userId, string firstName, string lastName, string passwordHash, string email, string phoneNumber,
        string addressLine1, string addressLine2, string city, string state, string zipCode, List<Order> orders) : this(userId, passwordHash, email)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        PasswordHash = passwordHash;
        Email = email;
        PhoneNumber = phoneNumber;
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        City = city;
        State = state;
        ZipCode = zipCode;
        Orders = orders ?? new List<Order>();
    }
    public static Result<User> CreateForRegistration(Guid userId, string passwordHash, string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return Result.Failure<User>($"'{nameof(email)}' can't be null or empty");
        }

        if (string.IsNullOrEmpty(passwordHash))
        {
            return Result.Failure<User>($"'{nameof(passwordHash)}' can't be null or empty");
        }

        var user = new User(userId, passwordHash, email);
        return Result.Success(user);
    }
    
    //todo Validation
    public static Result<User> Create(Guid userId, string firstName, string lastName, string passwordHash, string email,
        string phoneNumber, string addressLine1, string addressLine2, string city, string state, string zipCode,
        List<Order> orders) 
    {

        if (string.IsNullOrEmpty(email))
        {
            return Result.Failure<User>($"'{nameof(email)}' can't be null or empty");
        }

        if (string.IsNullOrEmpty(passwordHash))
        {
            return Result.Failure<User>($"'{nameof(passwordHash)}' can't be null or empty");
        }


        var user = new User(
            userId, firstName, lastName, passwordHash, email, phoneNumber, addressLine1, addressLine2, city, state,
            zipCode,
            orders);
        return Result.Success(user);
    }
}