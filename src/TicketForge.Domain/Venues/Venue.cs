using TicketForge.Domain.Common;

namespace TicketForge.Domain.Venues;

public sealed class Venue
{
    private readonly List<Seat> _seats = [];
    public string Name { get; }
    public IReadOnlyList<Seat> Seats => _seats;

    public Venue(string name, int rows, int seatsPerRow)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(rows);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(seatsPerRow);
        Name = name;
        for (int row = 1; row <= rows; row++)
        {
            for (int number = 1; number <= seatsPerRow; number++)
            {
                _seats.Add(new Seat(row, number));
            }
        }
    }

    public Seat GetSeat(int row, int number)=>
        _seats.SingleOrDefault(s => s.Row == row && s.Number == number)
        ?? throw new DomainException($"Seat {row}-{number} does not exist in venue {Name}.");
}