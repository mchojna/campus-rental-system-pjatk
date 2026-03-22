namespace CampusRentalSystem.Models.Users;

public class Student(string firstName, string lastName)
    : User(firstName, lastName)
{
    public static int _maxActiveRentals = 2;
    public static UserType _userType = UserType.Student;
}