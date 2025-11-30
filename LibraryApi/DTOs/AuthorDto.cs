using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryApi.DTOs
{
    public class AuthorDto
    {
        [Required]
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;
    }
}