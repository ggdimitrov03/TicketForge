using TicketForge.Domain.Common;
using TicketForge.Domain.Venues;

namespace TicketForge.Domain.Tests.Venues;

public class VenueTests
{
    [Fact]
    public void Constructor_CreatesCorrectNumberOfSeats()
    {
        var venue = new Venue("Arena Sofia", rows: 3, seatsPerRow: 4);

        Assert.Equal(12, venue.Seats.Count);
    }

    [Fact]
    public void GetSeat_ExistingSeat_ReturnsIt()
    {
        var venue = new Venue("Arena Sofia", 3, 4);

        var seat = venue.GetSeat(2, 3);

        Assert.Equal(2, seat.Row);
        Assert.Equal(3, seat.Number);
    }

    [Fact]
    public void GetSeat_NonExistingSeat_Throws()
    {
        var venue = new Venue("Arena Sofia", 3, 4);

        Assert.Throws<DomainException>(() => venue.GetSeat(10, 1));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithEmptyName_Throws(string name)
    {
        Assert.Throws<ArgumentException>(() => new Venue(name, 1, 1));
    }

    [Fact]
    public void AvailableSeatsCount_NewVenue_EqualsAllSeats()
    {
        var venue = new Venue("Arena Sofia", 3, 4);

        Assert.Equal(12, venue.AvailableSeatsCount);
    }

    [Fact]
    public void AvailableSeatsCount_AfterReserveAndSell_ExcludesThoseSeats()
    {
        var venue = new Venue("Arena Sofia", 3, 4);
        venue.GetSeat(1, 1).Reserve();
        var sold = venue.GetSeat(1, 2);
        sold.Reserve();
        sold.Sell();

        Assert.Equal(10, venue.AvailableSeatsCount);
    }
}
