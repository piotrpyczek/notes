using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Notes.Domain.Entities;

namespace Notes.Infrastructure.EntityConfigurations
{
    public class NoteEntityTypeConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Text);

            builder.HasMany(b => b.Tags)
                .WithMany(x => x.Notes)
                .UsingEntity("NoteTag");
        }
    }
}
