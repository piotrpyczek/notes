using System.Net;
using System.Text;
using System.Text.Json;
using Notes.Commands.Notes;
using Notes.Infrastructure.DataObjects;
using Notes.Infrastructure.TagResolver;

namespace Notes.IntegrationTests
{
    public class NotesControllerTest : IClassFixture<ApiWebApplicationFactory>
    {
        readonly ApiWebApplicationFactory application;

        public NotesControllerTest(ApiWebApplicationFactory application)
        {
            this.application = application;
        }

        [Fact]
        public async Task Get_all_notes_and_response_status_unauthorized()
        {
            var client = application.CreateClient();
            var response = await client.GetAsync("api/notes");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Get_all_notes_and_response_status_ok()
        {
            var client = application.CreateAutenticatedClient();
            var response = await client.GetAsync("api/notes");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Create_note_and_response_status_unauthorized()
        {
            var client = application.CreateClient();
            var content = new StringContent(BuildNote("test"), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/notes", content);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Create_note_and_response_status_ok()
        {
            var client = application.CreateAutenticatedClient();
            var content = new StringContent(BuildNote("test"), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/notes", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit")]
        public async Task Create_note_with_no_tags(string text)
        {
            var client = application.CreateAutenticatedClient();
            var content = new StringContent(BuildNote(text), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/notes", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var noteDTO = GetNoteDTO(responseString);

            Assert.NotNull(noteDTO);
            Assert.Empty(noteDTO.Tags);
        }

        [Theory]
        [InlineData("Lorem ipsum test@gmail.com dolor sit amet, consectetur adipiscing elit (+48)321654789")]
        public async Task Create_note_with_phone_and_email_tags(string text)
        {
            var client = application.CreateAutenticatedClient();
            var content = new StringContent(BuildNote(text), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/notes", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var noteDTO = GetNoteDTO(responseString);
            var emailTag = new EmailTagResolver().Name;
            var phoneTag = new PhoneTagResolver().Name;

            Assert.NotNull(noteDTO);
            Assert.Contains(noteDTO.Tags, tag => tag.Name == emailTag);
            Assert.Contains(noteDTO.Tags, tag => tag.Name == phoneTag);
        }

        private static string BuildNote(string text)
        {
            var note = new CreateNoteCommand
            {
                Text = text
            };

            return JsonSerializer.Serialize(note, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        private static NoteDTO? GetNoteDTO(string text)
        {
            return JsonSerializer.Deserialize<NoteDTO>(text, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}
