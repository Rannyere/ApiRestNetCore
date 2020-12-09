using IO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IO.Data.Mappings
{
    public class ProviderMapping : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Document)
                .IsRequired()
                .HasColumnType("varchar(14)");


            // 1 : 1 => Provider : Address
            builder.HasOne(p => p.Address)
                .WithOne(a => a.Provider);

            // 1 : N => Provider : Products
            builder.HasMany(providers => providers.Products)
                .WithOne(products => products.Provider)
                .HasForeignKey(products => products.ProviderId);

            builder.ToTable("Providers");
        }
    }
}

