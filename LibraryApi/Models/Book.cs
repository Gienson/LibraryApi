using LibraryApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Book
{
    public int Id { get; set; }

    [Required]
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [Range(0, int.MaxValue)]
    [JsonPropertyName("year")]
    public int Year { get; set; }

    [JsonPropertyName("author_id")]
    public int AuthorId { get; set; }

    [ForeignKey("AuthorId")]
    [JsonPropertyName("author")]
    public Author? Author { get; set; }


    [JsonPropertyName("items_ids")]
    public ICollection<Item> Items { get; set; } = new List<Item>();
}