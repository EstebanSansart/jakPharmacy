using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class ContactTypeConfiguration : IEntityTypeConfiguration<Contact_Type>
{
    public void Configure(EntityTypeBuilder<Contact_Type> builder){
        builder.ToTable("Contact_type");
        builder.HasKey(x => x.Id);
        
        // Properties

        builder.Property(x => x.Id)
            .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
            .HasColumnName("Id_contactType")
            .IsRequired();
    
        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasMaxLength(60)
            .IsRequired();

        builder.HasData(
            new{
                Id=1,
                Name = "staff"
            },
            new{
                Id=2,
                Name="profesional"
            },
            new{
                Id=3,
                Name="Provider"
            }
        );
    }
}