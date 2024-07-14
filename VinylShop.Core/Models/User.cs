using CSharpFunctionalExtensions;

namespace VinylShop.Core.Models;

public class User
{
    public Guid UserId { get; }
    public string FirstName { get; } = string.Empty;
    public string LastName { get; } = string.Empty;
    public string PasswordHash { get; } = string.Empty;
    public string Email { get; } = string.Empty;
    public string? PhoneNumber { get; } = string.Empty;
    public string? AddressLine1 { get; } = string.Empty;
    public string? AddressLine2 { get; } = string.Empty;
    public string? City { get; } = string.Empty;
    public string? State { get; } = string.Empty;
    public string? ZipCode { get; } = string.Empty;
    public List<Order>? Orders { get; }

    private User(Guid userId, string firstName, string lastName, string passwordHash, string email, string phoneNumber,
        string addressLine1, string addressLine2, string city, string state, string zipCode, List<Order> orders)
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

    //todo Validation
    public static Result<User> Create(Guid userId, string firstName, string lastName, string passwordHash, string email,
        string phoneNumber, string addressLine1, string addressLine2, string city, string state, string zipCode,
        List<Order> orders)
    {
        if (string.IsNullOrEmpty(firstName))
        {
            return Result.Failure<User>($"'{nameof(firstName)}' can't be null or empty");
        }

        if (string.IsNullOrEmpty(email))
        {
            return Result.Failure<User>($"'{nameof(email)}' can't be null or empty");
        }

        if (string.IsNullOrEmpty(phoneNumber))
        {
            return Result.Failure<User>($"'{nameof(phoneNumber)}' can't be null or empty");
        }

        if (string.IsNullOrEmpty(addressLine1))
        {
            return Result.Failure<User>($"'{nameof(addressLine1)}' can't be null or empty");
        }

        if (string.IsNullOrEmpty(addressLine2))
        {
            return Result.Failure<User>($"'{nameof(addressLine2)}' can't be null or empty");
        }

        if (string.IsNullOrEmpty(city))
        {
            return Result.Failure<User>($"'{nameof(city)}' can't be null or empty");
        }

        if (string.IsNullOrEmpty(state))
        {
            return Result.Failure<User>($"'{nameof(state)}' can't be null or empty");
        }

        if (string.IsNullOrEmpty(zipCode))
        {
            return Result.Failure<User>($"'{nameof(zipCode)}' can't be null or empty");
        }


        var user = new User(
            userId, firstName, lastName, passwordHash, email, phoneNumber, addressLine1, addressLine2, city, state,
            zipCode,
            orders);
        return Result.Success(user);
    }
}