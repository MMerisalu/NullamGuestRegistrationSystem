using App.Domain;
using Base.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
            relationship.DeleteBehavior = DeleteBehavior.Restrict;

    }
}