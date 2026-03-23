namespace CampusRentalSystem.Models.Users;

public abstract class User
{
    private static int _globalId = 0;

    protected User(string firstName, string lastName, UserType userType, int maxActiveRentals)
    {
        Id = _globalId++;

        FirstName = firstName;
        LastName = lastName;
        UserType = userType;
        MaxActiveRentals = maxActiveRentals;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;

        if (GetType() != obj.GetType()) return false;

        var other = (User)obj;
        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

    public override string ToString()
    {
        return $"User (Id={Id}, FirstName={FirstName}, LastName={LastName}, ActiveRentals={ActiveRentals})";
    }

    public int Id { get; }
    protected readonly string FirstName;
    protected readonly string LastName;
    public UserType UserType { get; }
    public int MaxActiveRentals { get; }
    public int ActiveRentals { get; set; } = 0;
}