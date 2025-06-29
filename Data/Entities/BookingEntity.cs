﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class BookingEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string EventId { get; set; } = null!;

    public int TicketQuantity { get; set; } = 1;

    public DateTime BookingDate { get; set; }

    [ForeignKey(nameof(BookingOwner))]
    public string? BookingOwnerId { get; set; }
    public BookingOwnerEntity? BookingOwner  { get; set; }
}
