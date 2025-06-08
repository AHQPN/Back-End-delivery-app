using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Backend_Mobile_App.Models;

namespace Backend_Mobile_App.Data
{
    public partial class Tracking_ShipmentContext : DbContext
    {
        public Tracking_ShipmentContext()
        {
        }

        public Tracking_ShipmentContext(DbContextOptions<Tracking_ShipmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Assignment> Assignments { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<ChatMessage> ChatMessages { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<Size> Sizes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.ToTable("Assignment");

                entity.HasIndex(e => e.VehicleId, "UQ__Assignme__476B54B366E5BCB3")
                    .IsUnique();

                entity.HasIndex(e => e.DeliveryPersonId, "UQ__Assignme__554C7AE7941B07E1")
                    .IsUnique();

                entity.Property(e => e.AssignmentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("AssignmentID")
                    .IsFixedLength();

                entity.Property(e => e.AssignedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeliveryPersonId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DeliveryPersonID")
                    .IsFixedLength();

                entity.Property(e => e.VehicleId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("VehicleID")
                    .IsFixedLength();

                entity.HasOne(d => d.DeliveryPerson)
                    .WithOne(p => p.Assignment)
                    .HasForeignKey<Assignment>(d => d.DeliveryPersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Assignmen__Deliv__52593CB8");

                entity.HasOne(d => d.Vehicle)
                    .WithOne(p => p.Assignment)
                    .HasForeignKey<Assignment>(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Assignmen__Vehic__534D60F1");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasIndex(e => e.CategoryName, "UQ__Category__8517B2E04AB7CBBB")
                    .IsUnique();

                entity.Property(e => e.CategoryId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CategoryID")
                    .IsFixedLength();

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.HasKey(e => e.MessageId)
                    .HasName("PK__ChatMess__C87C037C75EC1BC1");

                entity.ToTable("ChatMessage");

                entity.Property(e => e.MessageId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MessageID")
                    .IsFixedLength();

                entity.Property(e => e.IsRead).HasDefaultValueSql("((0))");

                entity.Property(e => e.ReceiverId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ReceiverID")
                    .IsFixedLength();

                entity.Property(e => e.SenderId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SenderID")
                    .IsFixedLength();

                entity.Property(e => e.SentAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.ChatMessageReceivers)
                    .HasForeignKey(d => d.ReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChatMessa__Recei__68487DD7");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.ChatMessageSenders)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK__ChatMessa__Sende__6754599E");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.NotificationId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("NotificationID")
                    .IsFixedLength();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsRead).HasDefaultValueSql("((0))");

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.UserId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("UserID")
                    .IsFixedLength();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Notificat__UserI__628FA481");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("OrderID")
                    .IsFixedLength();

                entity.Property(e => e.ActualDeliveryTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CustomerID")
                    .IsFixedLength();

                entity.Property(e => e.DeliveryPersonId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DeliveryPersonID")
                    .IsFixedLength();

                entity.Property(e => e.EstimatedDeliveryTime).HasColumnType("datetime");

                entity.Property(e => e.OrderStatus).HasMaxLength(20);

                entity.Property(e => e.PickupTime).HasColumnType("datetime");

                entity.Property(e => e.SdtnguoiNhan)
                    .HasMaxLength(100)
                    .HasColumnName("SDTNguoiNhan")
                    .IsFixedLength();

                entity.Property(e => e.Serviceid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TenNguoiNhan)
                    .HasMaxLength(100)
                    .HasColumnName("tenNguoiNhan");

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Orders__Customer__45F365D3");

                entity.HasOne(d => d.DestinationLocationNavigation)
                    .WithMany(p => p.OrderDestinationLocationNavigations)
                    .HasForeignKey(d => d.DestinationLocation)
                    .HasConstraintName("FK__Orders__Destinat__19DFD96B");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Serviceid)
                    .HasConstraintName("FK__Orders__Servicei__01142BA1");

                entity.HasOne(d => d.SourceLocationNavigation)
                    .WithMany(p => p.OrderSourceLocationNavigations)
                    .HasForeignKey(d => d.SourceLocation)
                    .HasConstraintName("FK__Orders__SourceLo__1BC821DD");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItem");

                entity.Property(e => e.OrderItemId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("OrderItemID")
                    .IsFixedLength();

                entity.Property(e => e.CategoryId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CategoryID")
                    .IsFixedLength();

                entity.Property(e => e.OrderId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("OrderID")
                    .IsFixedLength();

                entity.Property(e => e.SizeId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("sizeId")
                    .IsFixedLength();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__OrderItem__Categ__03F0984C");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__OrderItem__Order__48CFD27E");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.SizeId)
                    .HasConstraintName("FK__OrderItem__sizeI__02084FDA");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.HasIndex(e => e.OrderId, "UQ_Payment_OrderID")
                    .IsUnique();

                entity.Property(e => e.PaymentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PaymentID")
                    .IsFixedLength();

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("OrderID")
                    .IsFixedLength();

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Order)
                    .WithOne(p => p.Payment)
                    .HasForeignKey<Payment>(d => d.OrderId)
                    .HasConstraintName("FK__Payment__OrderID__5DCAEF64");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");

                entity.Property(e => e.ServiceId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ServiceID")
                    .IsFixedLength();

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("Size");

                entity.Property(e => e.SizeId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("SizeID")
                    .IsFixedLength();

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Weight).HasColumnName("weight");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserLocation, "UQ_User_LocationID")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Users__A9D1053408C052C8")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("UserID")
                    .IsFixedLength();

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.HasOne(d => d.UserLocationNavigation)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.UserLocation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__UserLocat__1CBC4616");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("Vehicle");

                entity.HasIndex(e => e.LicensePlate, "UQ__Vehicle__026BC15C04D34E5E")
                    .IsUnique();

                entity.Property(e => e.VehicleId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("VehicleID")
                    .IsFixedLength();

                entity.Property(e => e.LicensePlate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VehicleType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
