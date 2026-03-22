namespace CampusRentalSystem.Models.Users;

public class Employee(string firstName, string LastName)
    : User(firstName, LastName)
{
    public static int _maxActiveRentals = 5;
    public static UserType _userType = UserType.Employee;
}