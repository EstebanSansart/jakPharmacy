using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
{
    public void Configure(EntityTypeBuilder<Medicine> builder){
        builder.ToTable("Medicine");
        builder.HasKey(x => x.Id);
        
        // Properties
        builder.Property(x => x.Id)
            .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
            .HasColumnName("Id_Medicine")
            .IsRequired();

        builder.Property(x => x.Date_creation)
            .HasColumnName("Date_creation")
            .HasColumnType("DateTime")
            .IsRequired();

        builder.Property(x => x.Date_expiration)
            .HasColumnName("Date_expiration")
            .HasColumnType("DateTime")
            .IsRequired();
        
        builder.Property(x => x.Id_Date_expiration)
            .HasColumnName("Id_Date_expiration")
            .HasColumnType("int")
            .IsRequired();
        
        builder.Property(x => x.Id_medicine_info)
            .HasColumnName("Id_medicine_info")
            .HasColumnType("int")
            .IsRequired();

        // Keys

        builder.HasOne(x => x.Medicine_info)
            .WithMany(x => x.Medicines)
            .HasForeignKey(x => x.Id_medicine_info); 
        
        builder.HasOne(x => x.State)
            .WithMany(x => x.Medicines)
            .HasForeignKey(x => x.Id_estate); 

    }
    
}