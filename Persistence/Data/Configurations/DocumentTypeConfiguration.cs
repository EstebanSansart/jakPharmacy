using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class DocumentTypeConfiguration : IEntityTypeConfiguration<Document_type>
{
    public void Configure(EntityTypeBuilder<Document_type> builder){
        builder.ToTable("Document_type");
        builder.HasKey(x => x.Id);
        
        // Properties
        
        builder.Property(x => x.Id)
            .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
            .HasColumnName("Id_Document_type")
            .IsRequired();
    
        builder.Property(x => x.Description)
            .HasColumnName("Description")
            .HasMaxLength(60)
            .IsRequired();

        builder.HasData(                
            new{
                Id=1,
                Description = "CC"
            },
            new{
                Id=2,
                Description = "TI"
            },
            new{
                Id=3,
                Description = "NIT"
            }
        );
    }
}