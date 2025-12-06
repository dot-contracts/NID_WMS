// DEPRECATED: This DbContext is redundant and should not be used.
// Use wms_android.shared.Data.AppDbContext instead for consistency across projects.
// This file exists for backward compatibility but WILL BE REMOVED in future updates.
// All code should be migrated to use wms_android.shared.Data.AppDbContext instead.
// Any new code should use the shared AppDbContext.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wms_android.shared.Models;

namespace wms_android.data.Data
{
    [Obsolete("This class is deprecated. Use wms_android.shared.Data.AppDbContext instead.")]
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Drivers> Drivers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.VehicleRegistrationNumber).IsRequired();
                entity.Property(e => e.BodyType).IsRequired();
            });

            modelBuilder.Entity<Drivers>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.Property(e => e.Initials).IsRequired();
                entity.Property(e => e.LicenseNumber).IsRequired();
            });
            modelBuilder.Entity<Parcel>()
                .Property(p => p.CreatedAt)
                .HasColumnType("timestamp with time zone");

            modelBuilder.Entity<Parcel>()
                .Property(p => p.DispatchedAt)
                .HasColumnType("timestamp with time zone");

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasColumnType("timestamp without time zone");

                // Configure relationship with Role
                entity.HasOne(u => u.Role)
                    .WithMany()
                    .HasForeignKey(u => u.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle { Id = Guid.NewGuid(), VehicleRegistrationNumber = "KDL 085M", BodyType = "Truck" },
                new Vehicle { Id = Guid.NewGuid(), VehicleRegistrationNumber = "KCY 067A", BodyType = "Truck" },
                new Vehicle { Id = Guid.NewGuid(), VehicleRegistrationNumber = "KCZ 595L", BodyType = "Truck" },
                new Vehicle { Id = Guid.NewGuid(), VehicleRegistrationNumber = "KDB 387Q", BodyType = "Truck" },
                new Vehicle { Id = Guid.NewGuid(), VehicleRegistrationNumber = "KDE 228S", BodyType = "Truck" },
                new Vehicle { Id = Guid.NewGuid(), VehicleRegistrationNumber = "KAY 215H", BodyType = "Van" },
                new Vehicle { Id = Guid.NewGuid(), VehicleRegistrationNumber = "KBF 462A", BodyType = "Van" }
            );

            modelBuilder.Entity<Drivers>().HasData(
                new Drivers { Id = Guid.NewGuid(), FirstName = "John", LastName= "Njuguna", Initials="JN", LicenseNumber = "DL123456" },
                new Drivers { Id = Guid.NewGuid(), FirstName = "Robert", LastName= "Njuguna", Initials="RN", LicenseNumber = "DL123456" },
                new Drivers { Id = Guid.NewGuid(), FirstName = "John", LastName= "Mwai", Initials="JM", LicenseNumber = "DL123456" },
                new Drivers { Id = Guid.NewGuid(), FirstName = "David", LastName= "Mwangi", Initials="DM", LicenseNumber = "DL123456" },
                new Drivers { Id = Guid.NewGuid(), FirstName = "David", LastName= "Kibet", Initials="DK", LicenseNumber = "DL123456" },
                new Drivers { Id = Guid.NewGuid(), FirstName = "Erastus", LastName= "Kagwa", Initials="EK", LicenseNumber = "DL123456" },
                new Drivers { Id = Guid.NewGuid(), FirstName = "Julius", LastName= "Kamula", Initials="JK", LicenseNumber = "DL123456" },
                new Drivers { Id = Guid.NewGuid(), FirstName = "Stephen", LastName= "Kimuyu", Initials="SK", LicenseNumber = "DL123456" },
                new Drivers { Id = Guid.NewGuid(), FirstName = "Charles", LastName= "Maina", Initials="SK", LicenseNumber = "DL123456" },
                new Drivers { Id = Guid.NewGuid(), FirstName = "James", LastName= "Gichohi", Initials="SK", LicenseNumber = "DL123456" }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", Description = "Administrator" },
                new Role { Id = 2, Name = "Manager", Description = "Manager" },
                new Role { Id = 3, Name = "Clerk", Description = "Regular User" },
                new Role { Id = 4, Name = "Client", Description = "Client User" }
            );

            var createdAtDate = DateTime.SpecifyKind(new DateTime(2023, 1, 1), DateTimeKind.Utc);

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash("clerk123", out passwordHash, out passwordSalt);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@example.com",
                    Password = "admin123",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    RoleId = 1,
                    CreatedAt = createdAtDate
                },
                new User
                {
                    Id = 2,
                    Username = "manager",
                    Email = "manager@example.com",
                    Password = "manager123",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    RoleId = 2,
                    CreatedAt = createdAtDate
                },
                new User
                {
                    Id = 3,
                    Username = "clerk1",
                    Email = "clerk1@example.com",
                    Password = "clerk123",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    RoleId = 3,
                    CreatedAt = createdAtDate
                },
                new User
                {
                    Id = 4,
                    Username = "clerk2",
                    Email = "clerk2@example.com",
                    Password = "clerk123",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    RoleId = 3,
                    CreatedAt = createdAtDate
                },
                new User
                {
                    Id = 5,
                    Username = "client1",
                    Email = "client1@example.com",
                    Password = "client123",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    RoleId = 4,
                    CreatedAt = createdAtDate
                }
            );
            Console.WriteLine("Seed data configured for Users.");

            // Your existing DateTime conversion code
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties()
                    .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?)))
                {
                    // Skip setting the type for properties already configured with "timestamp with time zone"
                    if (property.Name == nameof(Parcel.CreatedAt) || property.Name == nameof(Parcel.DispatchedAt))
                        continue;

                    property.SetColumnType("timestamp without time zone");
                }
            }

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
