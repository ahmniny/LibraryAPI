using LibraryAPI.Models;

namespace LibraryAPI.dto
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        //public byte[] Cover { get; set; }
        public string? Description { get; set; }

        public int Year { get; set; }

        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
    }
}
