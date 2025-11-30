using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Author
{
    public int Id { get; set; }

    [Required]
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [JsonPropertyName("last_name")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("books")]
    public ICollection<Book> Books { get; set; } = new List<Book>();
}