namespace VinylShop.Core.Models;

public class User
{
    public Guid UserId { get; }
    public string FirstName { get; } = string.Empty;
    public string LastName { get; } = string.Empty;
    public string Email { get; } = string.Empty;
    public string PhoneNumber { get; } = string.Empty;
    public string AddressLine1 { get; } = string.Empty;
    public string AddressLine2 { get; } = string.Empty; 
    public string City { get; }= string.Empty;
    public string State { get; } = string.Empty; 
    public string ZipCode { get; } = string.Empty; 
    public List<Order>? Orders { get; } 

    private User(Guid userId, string firstName, string lastName, string email, string phoneNumber,
        string addressLine1, string addressLine2, string city, string state, string zipCode, List<Order> orders)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
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
    public static (User User, string Error) Create(Guid userId, string firstName, string lastName, string email,
        string phoneNumber, string addressLine1, string addressLine2, string city, string state, string zipCode,
        List<Order> orders)
    {
        var error = String.Empty;

        if (!string.IsNullOrEmpty(error))
        {
            return (null, error);
        }

        var user = new User(
            userId, firstName, lastName, email, phoneNumber, addressLine1, addressLine2, city, state, zipCode,
            orders);
        return (user, null);
    }
}