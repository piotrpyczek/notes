using Notes.Domain.Entities;

namespace Notes.UnitTests
{
    public class NoteTest
    {
        [Fact]
        public void create_note_success()
        {
            //Arrange
            var text = "test note";

            //Act
            var fakeNote = new Note(text);

            //Assert
            Assert.NotNull(fakeNote);
        }

        [Fact]
        public void create_note_fail()
        {
            //Arrange
            var text = string.Empty;

            //Act - Assert
            Assert.Throws<ArgumentNullException>(() => new Note(text));
        }
    }
}