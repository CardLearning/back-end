using CardLearning.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardLearning.Infrastructure.Database.Configurations;

public class LearningSessionProgressConfiguration : IEntityTypeConfiguration<LearningSessionProgress>
{
    public void Configure(EntityTypeBuilder<LearningSessionProgress> builder)
    {
        builder.ToTable("LearningSessionProgress");
        builder.HasKey(x => x.Id);
        
        builder.HasMany(x => x.CardResults)
            .WithOne()
            .HasForeignKey(x => x.LearningSessionProgressId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}