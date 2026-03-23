namespace CampusRentalSystem.Services;

public class PenaltyCalculator
{
    private const decimal DailyPenaltyRate = 5.0m;

    public decimal CalculatePenalty(DateOnly expectedReturnDate, DateOnly actualReturnDate)
    {
        if (actualReturnDate <= expectedReturnDate) return 0.0m;

        var daysLate = actualReturnDate.DayNumber - expectedReturnDate.DayNumber;
        return daysLate * DailyPenaltyRate;
    }
}