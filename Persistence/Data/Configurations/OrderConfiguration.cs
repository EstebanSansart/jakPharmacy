using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder){
        builder.ToTable("Order");
        builder.HasKey(x => x.Id);
        
        // Properties

        builder.Property(x => x.Id)
            .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
            .HasColumnName("Id_Order")
            .IsRequired();
    
        builder.Property(x => x.Order_Date)
            .HasColumnName("Order_Date")
            .HasColumnType("DateTime")
            .IsRequired();
        
        builder.Property(x => x.Detail)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.Date_expiration)
            .HasColumnName("Expire_Date")
            .HasColumnType("DateTime")
            .IsRequired();

        // Keys
        builder.HasOne(x => x.Sale)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.Id_sale);

        builder.HasOne(x => x.Eps)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.EpsId);    

             builder.HasData(
              
                new Order
                {
                    Id = 1,
                    Order_Date = new DateTime(2022, 5, 1),
                    Detail = "Order detail 1",
                    Date_expiration = new DateTime(2023, 5, 15),
                    Id_sale = 1,
                    EpsId = 1
                },
                new Order
                {
                    Id = 2,
                    Order_Date = new DateTime(2023, 5, 3),
                    Detail = "Order detail 2",
                    Date_expiration = new DateTime(2023, 5, 20),
                    Id_sale = 2,
                    EpsId = 2
                }
            );
    }

    
    
}