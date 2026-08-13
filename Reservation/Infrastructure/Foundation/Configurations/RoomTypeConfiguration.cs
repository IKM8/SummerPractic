using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;

public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
{
    public void Configure( EntityTypeBuilder<RoomType> builder )
    {
        builder.ToTable( "RoomTypes" );
        builder.HasKey( r => r.Id );

        builder.Property( r => r.Name ).HasMaxLength( 100 ).IsRequired();
        builder.Property( r => r.Currency ).HasMaxLength( 10 ).IsRequired();
        builder.Property( r => r.DailyPrice ).HasPrecision( 18, 2 ).IsRequired();
    }
}