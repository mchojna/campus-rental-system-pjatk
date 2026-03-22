namespace CampusRentalSystem.Models.Users;

public class Student(string firstName, string lastName)
    : User(firstName, lastName)
{
    private static int _maxActiveRentals = 2;
    private static UserType _userType = UserType.Student;
}