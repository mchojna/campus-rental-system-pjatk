namespace CampusRentalSystem.Models.Users;

public class Student(string firstName, string lastName)
    : User(firstName, lastName, UserType.Student, 2)
{
    public override string ToString()
    {
        return
            $"Student (Id={Id}, FirstName={FirstName}, LastName={LastName}, ActiveRentals={ActiveRentals}, MaxActiveRentals={MaxActiveRentals}, UserType={UserType})";
    }
}