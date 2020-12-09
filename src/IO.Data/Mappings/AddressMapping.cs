using IO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IO.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(a => a.PublicPlace)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(a => a.Number)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(a => a.ZipCode)
                .IsRequired()
                .HasColumnType("varchar(8)");

            builder.Property(a => a.Complement)
                .HasColumnType("varchar(250)");

            builder.Property(a => a.Neighborhoodty)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(a => a.City)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(a => a.State)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.ToTable("Adresses");
        }
    }
}
