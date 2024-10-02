using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelMan2.Models;

public partial class HotelContext : IdentityDbContext<AppUser>
{
    public HotelContext()
    {
    }

    public HotelContext(DbContextOptions<HotelContext> options) : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Housekeeping> Housekeepings { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomStatus> RoomStatuses { get; set; }

    public virtual DbSet<RoomType> RoomTypes { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=(LocalDb)\\MSSQLLocalDB;Initial Catalog=hotel;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.ToTable("booking");

            entity.Property(e => e.BookingId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("booking_id");
            entity.Property(e => e.CheckIn).HasColumnName("check_in");
            entity.Property(e => e.CheckOut).HasColumnName("check_out");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.TotalAmmount)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("total_ammount");

            entity.HasOne(d => d.Person).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_booking_person");

            entity.HasOne(d => d.Room).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_booking_room");
        });

        modelBuilder.Entity<Housekeeping>(entity =>
        {
            entity.ToTable("housekeeping");

            entity.Property(e => e.HousekeepingId).HasColumnName("housekeeping_id");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.HousekeepingDate).HasColumnName("housekeeping_date");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.RoomId).HasColumnName("room_id");

            entity.HasOne(d => d.Person).WithMany(p => p.Housekeepings)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_housekeeping_person");

            entity.HasOne(d => d.Room).WithMany(p => p.Housekeepings)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_housekeeping_room");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("person");

            entity.Property(e => e.PersonId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("person_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Surename)
                .HasMaxLength(50)
                .HasColumnName("surename");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("user_name");

            entity.HasOne(d => d.Role).WithMany(p => p.People)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_person_role");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.ToTable("room");

            entity.Property(e => e.RoomId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("room_id");
            entity.Property(e => e.RoomNumber)
                .HasMaxLength(50)
                .HasColumnName("room_number");
            entity.Property(e => e.RoomTypeId).HasColumnName("room_type_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.RoomType).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.RoomTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_room_room_type");

            entity.HasOne(d => d.Status).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_room_room_status");
        });

        modelBuilder.Entity<RoomStatus>(entity =>
        {
            entity.ToTable("room_status");

            entity.Property(e => e.RoomStatusId).HasColumnName("room_status_id");
            entity.Property(e => e.RoomStatusName)
                .HasMaxLength(20)
                .HasColumnName("room_status_name");
        });

        modelBuilder.Entity<RoomType>(entity =>
        {
            entity.ToTable("room_type");

            entity.Property(e => e.RoomTypeId).HasColumnName("room_type_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("price");
            entity.Property(e => e.RoomTypeName)
                .HasMaxLength(50)
                .HasColumnName("room_type_name");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_user_roles");

            entity.ToTable("user_roles");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
