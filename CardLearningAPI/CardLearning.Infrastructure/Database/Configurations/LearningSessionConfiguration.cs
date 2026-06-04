using CardLearning.Domain;
using CardLearning.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardLearning.Infrastructure.Database.Configurations;

public class LearningSessionConfiguration : IEntityTypeConfiguration<LearningSession>
{
    public void Configure(EntityTypeBuilder<LearningSession> builder)
    {
        builder.ToTable("LearningSession");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Status)
            .HasDefaultValue(LearningSessionEnum.Started);

        builder.Property(x => x.StartDate)
            .HasDefaultValueSql("SYSUTCDATETIME()");
        
        builder.HasOne(x => x.Deck)
            .WithMany()
            .HasForeignKey(x => x.DeckId);
    }
}

