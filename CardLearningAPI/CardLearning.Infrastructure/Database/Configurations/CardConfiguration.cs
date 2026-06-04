using CardLearning.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardLearning.Infrastructure.Database.Configurations;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.ToTable("Card");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Front)
            .HasMaxLength(300);
        builder.Property(x => x.Back)
            .HasMaxLength(300);

    }
}