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
    
    protected int Id { get; set; }
    protected string FirstName { get; set; }
    protected string LastName { get; set; }
    protected int ActiveRentals { get; set; } = 0;
}