using CardLearning.Domain;
using CardLearning.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardLearning.Infrastructure.Database.Configurations;

public class LearningSessionCardResultConfiguration : IEntityTypeConfiguration<LearningSessionCardResult>
{
    public void Configure(EntityTypeBuilder<LearningSessionCardResult> builder)
    {
        builder.ToTable("LearningSessionCardResult");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasDefaultValue(CardResultSessionStatus.NotStarted);
        
        builder.HasIndex(x => new { x.LearningSessionProgressId, x.CardId })
            .IsUnique();
        
        builder.HasOne(x => x.Card)
            .WithMany()
            .HasForeignKey(x => x.CardId)
            .OnDelete(DeleteBehavior.Restrict);
           
    }
}