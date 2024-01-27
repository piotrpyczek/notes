using Notes.Domain.Entities;
using Notes.Infrastructure.DataObjects;

namespace Notes.Infrastructure.Converters
{
    public static class TagConverter
    {
        public static TagDTO ToTagDTO(this Tag tag)
        {
            return new TagDTO
            {
                Id = tag.Id,
                Name = tag.Name,
            };
        }
    }
}
