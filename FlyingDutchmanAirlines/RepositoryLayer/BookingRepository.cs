using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines.RepositoryLayer
{
    public class BookingRepository
    {
        private readonly FlyingDutchmanAirlinesContext _context;
        public BookingRepository(FlyingDutchmanAirlinesContext context)
        {
            _context = context;
        }

        public async Task CreateBooking(int CustomerId, int flightNumber)
        {
            if (CustomerId < 0 || flightNumber < 0)
            {
                Console.WriteLine($"Argument Exception in CreatBooking! CustomerId = {CustomerId}, flightNumber = {flightNumber}");
                throw new ArgumentException("Invalid arguments provided");
            }

            Booking newBooking = new Booking
            {
                CustomerId = CustomerId,
                FlightNumber = flightNumber
            };

            try
            {
                _context.Bookings.Add(newBooking);
                await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Exception during database query: {exception.Message}");
                throw new CouldNotAddBookingToDatabaseException();
            }
        }

    }
}
