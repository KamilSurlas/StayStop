using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StayStop.Model;
using StayStop.Model.Enums;

namespace StayStop.DAL.Context
{
    public class StayStopDbContext : DbContext
    {
        public DbSet<Opinion> Opinions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationPosition> ReservationPositions { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Role> Roles { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StayStopDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>()
            .HasMany(h => h.Managers)
            .WithMany(u => u.ManagedHotels)
            .UsingEntity(j => j.ToTable("HotelManagers"));

            modelBuilder.Entity<Hotel>()
                .HasOne(h => h.Owner)
                .WithMany(u => u.OwnedHotels);


            // Cena w postaci (10000.00 zl)
            modelBuilder.Entity<Room>().Property(r => r.PriceForAdult).HasColumnType("decimal(7,2)");
            modelBuilder.Entity<Room>().Property(r => r.PriceForChild).HasColumnType("decimal(7,2)");
            modelBuilder.Entity<Reservation>().Property(r => r.Price).HasColumnType("decimal(7,2)");
            modelBuilder.Entity<ReservationPosition>().Property(rp => rp.Price).HasColumnType("decimal(7,2)");

            // Enumy jako tekst nie inty
            modelBuilder.Entity<Hotel>().Property(h => h.HotelType).HasConversion(new EnumToStringConverter<HotelType>());
            modelBuilder.Entity<Room>().Property(r => r.RoomType).HasConversion(new EnumToStringConverter<RoomType>());
            modelBuilder.Entity<Reservation>().Property(r => r.ReservationStatus).HasConversion(new EnumToStringConverter<ReservationStatus>());
     
            //ReservationPosition - Rooms
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

            //Rooms - Hotel
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
