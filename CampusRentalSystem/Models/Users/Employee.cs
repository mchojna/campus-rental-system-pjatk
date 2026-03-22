namespace CampusRentalSystem.Models.Users;

public class Employee(string firstName, string lastName)
    : User(firstName, lastName, UserType.Employee, 5)
{
    public override string ToString()
    {
        return
            $"Employee (Id={Id}, FirstName={FirstName}, LastName={LastName}, ActiveRentals={ActiveRentals}, MaxActiveRentals={MaxActiveRentals}, " +
            $"UserType={UserType})";
    }
}