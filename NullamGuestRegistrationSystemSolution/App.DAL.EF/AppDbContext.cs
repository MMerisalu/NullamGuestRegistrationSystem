using App.Domain;
using Base.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace App.DAL.EF;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        this.ChangeTracker.LazyLoadingEnabled = false;
    }

    public DbSet<PaymentMethod> PaymentMethods { get; set; } = default!;
    public DbSet<Event> Events { get; set; } = default!;
    public DbSet<EventAndAttendee> EventsAndAttendees { get; set; } = default!;
    public DbSet<Attendee> Attendees { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        foreach (var relationship in builder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.Cascade;
       // builder.Entity<EventAndAttendee>()
       //.HasOne(ea => ea.Event)  // Each EventAndAttendee is related to one Event
       //.WithMany(e => e.Attendees)  // An Event has many EventAndAttendees (representing its Attendees)
       //.HasForeignKey(ea => ea.EventId)  // The foreign key in EventAndAttendee that references Event
       //.OnDelete(DeleteBehavior.Restrict);  // Don't cascade delete from Event to EventAndAttendee

       // builder.Entity<EventAndAttendee>()
       //     .HasOne(ea => ea.Attendee)  // Each EventAndAttendee is related to one Attendee
       //     .WithMany(a => a.Events)  // An Attendee has many EventAndAttendees (representing its Events)
       //     .HasForeignKey(ea => ea.AttendeeId)  // The foreign key in EventAndAttendee that references Attendee
       //     .OnDelete(DeleteBehavior.Cascade);  // Cascade delete from Attendee to EventAndAttendee
    }

}
