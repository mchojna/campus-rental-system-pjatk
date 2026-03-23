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
        Status = RentalStatus.Active;
    }

    public readonly int Id;
    public User User { get; }
    public Device Device { get; }
    public DateOnly RentalDate { get; }
    public DateOnly ExpectedReturnDate { get; }
    public DateOnly? ActualReturnDate { get; set; } = null;
    public RentalStatus Status { get; set; }

    public void MarkReturned(DateOnly actualReturnDate)
    {
        ActualReturnDate = actualReturnDate;
        Status = RentalStatus.Returned;
    }

    public override string ToString()
    {
        return
            $"Rental (Id={Id}, User={User.Id}, Device={Device.Id}, RentalDate={RentalDate}, ExpectedReturnDate={ExpectedReturnDate}, " +
            $"ActualReturnDate={(ActualReturnDate.HasValue ? ActualReturnDate.Value.ToString() : "Not returned yet")}, Status={Status})";
    }
}