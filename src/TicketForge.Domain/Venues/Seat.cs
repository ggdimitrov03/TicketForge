using TicketForge.Domain.Common;

namespace TicketForge.Domain.Venues;
public sealed class Seat
{
    public int Row { get; }
    public int Number { get; }
    public SeatStatus Status { get; private set; }

    public Seat(int row, int number)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(row);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(number);
        Row = row;
        Number = number;
        Status = SeatStatus.Available;
    }

    public void Reserve()
    {
        if (Status != SeatStatus.Available)
        {
            throw new DomainException($"Seat {Row}-{Number} is {Status} and cannot be reserved.");
        }
        Status = SeatStatus.Reserved;
    }

    public void Sell()
    {
        if (Status != SeatStatus.Reserved)
        {
            throw new DomainException($"Seat {Row}-{Number} is {Status} and cannot be sold.");
        }
        Status = SeatStatus.Sold;
    }

    public void Release()
    {
        if (Status != SeatStatus.Reserved)
        {
            throw new DomainException($"Seat {Row}-{Number} is {Status} and cannot be released.");
        }
        Status = SeatStatus.Available;
    }

}