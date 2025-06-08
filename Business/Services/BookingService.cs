using Business.Models;
using Data.Entities;
using Data.Repositories;

namespace Business.Services;

public class BookingService(IBookingRepository bookingRepository) : IBookingService
{
    private readonly IBookingRepository _bookingRepository = bookingRepository;

    public async Task<BookingResult> CreateBookingAsync(CreateBookingRequest request)
    {
        try
        {
            var bookingEntity = new BookingEntity
            {
                EventId = request.EventId,
                BookingDate = DateTime.Now,
                TicketQuantity = request.TicketQuantity,
                BookingOwner = new BookingOwnerEntity
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Address = new BookingAddressEntity
                    {
                        StreetName = request.StreetName,
                        PostalCode = request.PostalCode,
                        City = request.City,
                    }
                }
            };

            var result = await _bookingRepository.AddAsync(bookingEntity);

            //This is the first draft for using an email service in Azure to send a Verification Email 
            //To Do: Next step is to create an EmailService microservice and publish it to Azure. That service will then be called on to send the e-mail.
            EmailService.SendEmail(bookingEntity.BookingOwner.Email, bookingEntity.EventId, bookingEntity.TicketQuantity);

            return result.Success
                ? new BookingResult { Success = true }
                : new BookingResult { Success = false, Error = result.Error };

        }
        catch (Exception ex)
        {
            return new BookingResult
            {
                Success = false,
                Error = ex.Message
            };
        }
    }
}