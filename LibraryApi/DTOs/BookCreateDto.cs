using System.ComponentModel.DataAnnotations;

namespace LibraryApi.DTOs
{
    public class BookCreateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int Year { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }
}