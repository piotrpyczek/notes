using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Notes.Domain.Entities;

namespace Notes.Infrastructure.EntityConfigurations
{
    public class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name);

            builder.HasMany(b => b.Notes)
                .WithMany(x => x.Tags)
                .UsingEntity("NoteTag");
        }
    }
}