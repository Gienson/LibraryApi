using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryApi.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("first_name")] // To ważne dla testów - zmienia nazwę w JSON
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;

        // Relacja: jeden autor, wiele książek
        [JsonIgnore] // Ukrywamy to w API, żeby nie zapętlić danych
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}