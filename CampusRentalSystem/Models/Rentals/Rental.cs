using CampusRentalSystem.Models.Devices;
using CampusRentalSystem.Models.Users;

namespace CampusRentalSystem.Models.Rentals;

public class Rental
{
    public Rental(User user, Device device, DateOnly rentedFrom, DateOnly rentedTo)
    {
        User = user;
        Device = device;
        RentedFrom = rentedFrom;
        RentedTo = rentedTo;
    }

    public User User { get; set; }
    public Device Device { get; set; }
    public DateOnly RentedFrom { get; set; } 
    public DateOnly RentedTo { get; set; } 
}