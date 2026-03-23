using CampusRentalSystem.Exceptions;
using CampusRentalSystem.Models.Devices;
using CampusRentalSystem.Models.Users;
using CampusRentalSystem.Services;

var service = new Service(new PenaltyCalculator());

// Add devices
var laptop1 = new Laptop("ThinkPad T14", "Lenovo", 1.5, new DateOnly(2024, 1, 15),
    "Ryzen 7", "16GB", "512GB", "Windows 11");

var laptop2 = new Laptop("MacBook Pro", "Apple", 1.4, new DateOnly(2024, 3, 2),
    "M3", "18GB", "512GB", "macOS");

var projector1 = new Projector("PowerLite", "Epson", 2.8, new DateOnly(2023, 10, 10),
    "LCD", "1920x1080", "3500 lm", "16000:1");

var camera1 = new Camera("EOS R8", "Canon", 0.46, new DateOnly(2024, 5, 5),
    1.8, 1.0 / 8000, 100, "Full Frame CMOS");

service.AddNewDevice(laptop1);
service.AddNewDevice(laptop2);
service.AddNewDevice(projector1);
service.AddNewDevice(camera1);

// Try to add duplicate device
try
{
    service.AddNewDevice(laptop1);
}
catch (DomainException ex)
{
    Console.WriteLine($"\n{ex.Message}");
}

// Add users
var student = new Student("Jan", "Kowalski");
var employee = new Employee("Anna", "Nowak");

service.AddNewUser(student);
service.AddNewUser(employee);

// Try to add duplicate user
try
{
    service.AddNewUser(student);
}
catch (DomainException ex)
{
    Console.WriteLine($"\n{ex.Message}");
}

Console.WriteLine("\nAll device: ");
PrintCollection(service.GetAllDevices());

Console.WriteLine("\nAvailable devices: ");
PrintCollection(service.GetAvailableDevices());

// Rent device
var rentalDate = new DateOnly(2026, 3, 1);
var expectedReturn = new DateOnly(2026, 3, 7);
service.RentDevice(student.Id, laptop1.Id, rentalDate, expectedReturn);
Console.WriteLine($"\nStudent with Id={student.Id} rented laptop with Id={laptop1.Id}.");

// Try to rent unavailable device
try
{
    service.RentDevice(employee.Id, laptop1.Id, rentalDate, expectedReturn);
}
catch (DomainException ex)
{
    Console.WriteLine($"\n{ex.Message}");
}

// Send device to repair and try to rent it
service.SendToRepair(projector1.Id);
Console.WriteLine($"\nDevice with Id={projector1.Id} sent to repair.");
try
{
    service.RentDevice(employee.Id, projector1.Id, rentalDate, expectedReturn);
}
catch (DomainException ex)
{
    Console.WriteLine($"\n{ex.Message}");
}

// Collect device from repair and rent again
service.CollectFromRepair(projector1.Id);
Console.WriteLine($"\nDevice with Id={projector1.Id} collected from repair.");

service.RentDevice(employee.Id, projector1.Id, new DateOnly(2026, 3, 8), new DateOnly(2026, 3, 11));
Console.WriteLine($"\nEmployee with Id={employee.Id} rented projector with Id={projector1.Id}.");

// Try to exceed rental limit
service.RentDevice(student.Id, laptop2.Id, rentalDate, expectedReturn);
try
{
    service.RentDevice(student.Id, projector1.Id, rentalDate, expectedReturn);
}
catch (DomainException ex)
{
    Console.WriteLine($"\n{ex.Message}");
}

// Return on time
var onTimeReturnPenalty = service.ReturnDevice(student.Id, laptop1.Id, new DateOnly(2026, 3, 7));
Console.WriteLine($"\nOn-time return. Penalty: {onTimeReturnPenalty} PLN");

// Late return with penalty
service.RentDevice(employee.Id, camera1.Id, new DateOnly(2026, 3, 10), new DateOnly(2026, 3, 12));
var lateReturnPenalty = service.ReturnDevice(employee.Id, camera1.Id, new DateOnly(2026, 3, 15));
Console.WriteLine($"\nLate return. Penalty: {lateReturnPenalty} PLN");

// Try to return device that is not actively rented
try
{
    service.ReturnDevice(student.Id, camera1.Id, new DateOnly(2026, 3, 16));
}
catch (DomainException ex)
{
    Console.WriteLine($"\n{ex.Message}");
}

// Return the projector to clean up active rentals
var projectorPenalty = service.ReturnDevice(employee.Id, projector1.Id, new DateOnly(2026, 3, 12));
Console.WriteLine($"\nProjector return. Penalty: {projectorPenalty} PLN");

Console.WriteLine("\nStudent active rentals:");
PrintCollection(service.GetUsersActiveRentals(student.Id));

Console.WriteLine("\nOverdue rentals (2026-03-20):");
PrintCollection(service.GetOverdueRentals(new DateOnly(2026, 3, 20)));

// Try to get rentals for a missing user
try
{
    PrintCollection(service.GetUsersActiveRentals(999));
}
catch (DomainException ex)
{
    Console.WriteLine($"\n{ex.Message}");
}

// Show final report
var report = service.GenerateReport();
Console.WriteLine("\nGenerated report:");
PrintReport(report);

return;

static void PrintCollection<T>(IEnumerable<T> items)
{
    var materialized = items.ToList();
    if (materialized.Count == 0)
    {
        Console.WriteLine("\nNo data.");
        return;
    }

    materialized.ForEach(i => Console.WriteLine($"\t{i}"));
}

static void PrintReport((int TotalUsers, int TotalDevices, int AvailableDevices, int DamagedDevices, int TotalActiveRentals) report)
{
    Console.WriteLine($"\tTotal users: {report.TotalUsers}");
    Console.WriteLine($"\tTotal devices: {report.TotalDevices}");
    Console.WriteLine($"\tAvailable devices: {report.AvailableDevices}");
    Console.WriteLine($"\tDamaged devices: {report.DamagedDevices}");
    Console.WriteLine($"\tActive rentals: {report.TotalActiveRentals}");
}