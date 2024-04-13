using Microsoft.EntityFrameworkCore;
using StayStop.Model;

namespace StayStop.DAL.Context
{
    public class StayStopDbContext : DbContext
    {
        public DbSet<Opinion> Opinions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationPosition> ReservationPositions { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Hotel> Hotels { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StayStopDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ReservationPosition - Room
            modelBuilder.Entity<ReservationPosition>()
                .HasOne(rp => rp.Room)
                .WithMany(r => r.ReservationPositions)
                .OnDelete(DeleteBehavior.Restrict);

            //ReservationPosition - Reservation
            modelBuilder.Entity<ReservationPosition>()
                .HasOne(rp => rp.Reservation)
                .WithMany(r => r.ReservationPositions)
                .OnDelete(DeleteBehavior.Cascade);

            //Opinion - Reservation
            modelBuilder.Entity<Opinion>()
                .HasOne(o => o.Reservation)
                .WithOne(r => r.Opinion)
                .OnDelete(DeleteBehavior.Cascade);

            //Reservation - User
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.UserReservations)
                .OnDelete(DeleteBehavior.Cascade);

            //Room - Hotel
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
