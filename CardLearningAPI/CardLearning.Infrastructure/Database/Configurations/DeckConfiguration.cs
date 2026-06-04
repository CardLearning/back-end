using CardLearning.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardLearning.Infrastructure.Database.Configurations;

public class DeckConfiguration : IEntityTypeConfiguration<Deck>
{
    public void Configure(EntityTypeBuilder<Deck> builder)
    {
        builder.ToTable("Deck");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(x => x.Description)
            .HasColumnType("nvarchar(max)");

        builder.HasMany(x => x.Cards)
            .WithOne()
            .HasForeignKey(x => x.DeckId);

    }
}