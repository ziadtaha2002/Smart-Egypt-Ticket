using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Smart_Egypt_Ticket.Models;

namespace Smart_Egypt_Ticket.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Monument> Monuments { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderNumber)
                .IsUnique();

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(item => item.TicketType)
                .WithMany()
                .HasForeignKey(item => item.TicketTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasIndex(p => new { p.Provider, p.ProviderReference })
                .IsUnique()
                .HasFilter("[ProviderReference] IS NOT NULL");

            modelBuilder.Entity<Ticket>()
                .HasIndex(t => t.TicketNumber)
                .IsUnique();

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.UsedByUser)
                .WithMany()
                .HasForeignKey(t => t.UsedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AuditLog>()
                .HasOne(log => log.User)
                .WithMany()
                .HasForeignKey(log => log.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<OrderItem>()
                .Property(item => item.LineTotal)
                .HasComputedColumnSql("[Quantity] * [UnitPrice]");
        }
    }
}