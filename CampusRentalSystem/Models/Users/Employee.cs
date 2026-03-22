namespace CampusRentalSystem.Models.Users;

public class Employee(string firstName, string LastName)
    : User(firstName, LastName)
{
    private static int _maxActiveRentals = 5;
    private static UserType _userType = UserType.Employee;
}