using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryApi.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Rok nie może być ujemny")]
        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonIgnore]
        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        [JsonPropertyName("author")]
        public Author? Author { get; set; }

        [JsonIgnore]
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}