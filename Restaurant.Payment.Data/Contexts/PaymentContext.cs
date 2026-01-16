using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Restaurant.Payment.Data.Model;

namespace Restaurant.Payment.Data.Contexts;

public class PaymentContext(IConfiguration configuration) : DbContext
{

    private readonly string connectionString = configuration.GetConnectionString("database")
        ?? throw new ArgumentNullException("database");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderData>(e =>
        {
            e.ToTable("Order");
            e.HasKey(o => o.Id);
        });

        modelBuilder.Entity<OrderItemData>(e =>
        {
            e.ToTable("OrderItem");
            e.HasKey(o => o.Id);
            e.HasOne(o => o.Order)
                .WithMany(o => o.Items);
        });

        modelBuilder.Entity<DefaultAddressData>(e =>
        {
            e.ToTable("DefaultAddress");
            e.HasKey(o => o.Id);
        });

        modelBuilder.Entity<PaymentInfoData>(e =>
        {
            e.ToTable("PaymentInfo");
            e.HasKey(o => o.Id);
            e.Property(o => o.Id)
                .ValueGeneratedOnAdd();
            e.HasOne(o => o.Order)
                .WithMany(o => o.PaymentInfo);
        });
    }

}
