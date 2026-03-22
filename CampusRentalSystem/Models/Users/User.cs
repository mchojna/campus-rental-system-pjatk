namespace CampusRentalSystem.Models.Users;

public abstract class User
{
    private static int _globalId = 0;

    protected User(string firstName, string lastName)
    {
        Id = _globalId++;

        FirstName = firstName;
        LastName = lastName;
    }

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int ActiveRentals { get; set; } = 0;
}