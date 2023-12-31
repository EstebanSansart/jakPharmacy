using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    private static readonly Random _random = new();
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sale");
        builder.HasKey(x => x.Id);

        // Properties

        builder.Property(x => x.Id)
            .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
            .HasColumnName("Id_Sale")
            .IsRequired();

        builder.Property(x => x.Sale_Date)
            .HasColumnName("Sale_Date")
            .HasColumnType("Datetime")
            .IsRequired();

        // Keys

        builder.HasOne(x => x.Employee)
            .WithMany(x => x.Sales)
            .HasForeignKey(x => x.Id_Employe);

        builder.HasOne(x => x.Person)
            .WithMany(x => x.Sales)
            .HasForeignKey(x => x.Id_person);

        // Sale - Medicine

        builder
        .HasMany(r => r.Medicines)
        .WithMany(p => p.Sales)
        .UsingEntity<Detail_sale>(

            j => j
            .HasOne(pt => pt.Medicine)
            .WithMany(t => t.Detail_Sales)
            .HasForeignKey(ut => ut.Id_medicine),

            j => j
            .HasOne(et => et.Sale)
            .WithMany(e => e.Detail_Sales)
            .HasForeignKey(te => te.Id_sale),

            j =>
            {
                j.ToTable("Detail_Sale");
                j.HasKey(t => new{t.Id_medicine, t.Id_sale}); 
            }
            );

            builder.HasData(
                GetSaleSeed()
            );
    }
    private static List<Sale> GetSaleSeed()
    {
        List<Sale> SalesList = new();
        for (int i = 1; i <= 16; i++)
        {
            SalesList.Add(new Sale
            {
                Id = i,
                Sale_Date = new DateTime(2023, 5, 5).AddDays(_random.Next(1, 365)),
                Id_person = _random.Next(1, 4),
                Id_Employe = _random.Next(1, 4)
            });
        }
        return SalesList;
    }
}