using CampusRentalSystem.Models.Devices;
using CampusRentalSystem.Models.Users;

namespace CampusRentalSystem.Models.Rentals;

public class Rental
{
    private static int _globalId = 0;
    
    public Rental(User user, Device device, DateOnly rentalDate, DateOnly expectedReturnDate)
    {
        Id = _globalId++;
        
        User = user;
        Device = device;
        RentalDate = rentalDate;
        ExpectedReturnDate = expectedReturnDate;
    }

    protected readonly int Id;
    public User User { get; }
    public Device Device { get; }
    public DateOnly RentalDate { get; }
    public DateOnly ExpectedReturnDate { get; }
    public DateOnly? ActualReturnDate { get; set; } = null;

    // faktyczny zwrot / kara
    // jak dlugo
}