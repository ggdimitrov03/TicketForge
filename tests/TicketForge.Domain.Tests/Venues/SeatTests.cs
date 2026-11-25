using TicketForge.Domain.Common;
using TicketForge.Domain.Venues;

namespace TicketForge.Domain.Tests.Venues;

public class SeatTests
{
    [Fact]
    public void NewSeat_IsAvailable()
    {
        var seat = new Seat(1, 1);

        Assert.Equal(SeatStatus.Available, seat.Status);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    [InlineData(-1, 5)]
    public void Constructor_WithInvalidValues_Throws(int row, int number)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Seat(row, number));
    }

    [Fact]
    public void Reserve_AvailableSeat_BecomesReserved()
    {
        var seat = new Seat(1, 1);

        seat.Reserve();

        Assert.Equal(SeatStatus.Reserved, seat.Status);
    }

    [Fact]
    public void Reserve_AlreadyReservedSeat_Throws()
    {
        var seat = new Seat(1, 1);
        seat.Reserve();

        Assert.Throws<DomainException>(() => seat.Reserve());
    }

    [Fact]
    public void Sell_ReservedSeat_BecomesSold()
    {
        var seat = new Seat(1, 1);
        seat.Reserve();

        seat.Sell();

        Assert.Equal(SeatStatus.Sold, seat.Status);
    }

    [Fact]
    public void Sell_AvailableSeat_Throws()
    {
        var seat = new Seat(1, 1);

        Assert.Throws<DomainException>(() => seat.Sell());
    }

    [Fact]
    public void Release_SoldSeat_Throws()
    {
        var seat = new Seat(1, 1);
        seat.Reserve();
        seat.Sell();

        Assert.Throws<DomainException>(() => seat.Release());
    }

    [Fact]
    public void Release_ReservedSeat_BecomesAvailable()
    {
        var seat = new Seat(1, 1);
        seat.Reserve();

        seat.Release();

        Assert.Equal(SeatStatus.Available, seat.Status);
    }
}
