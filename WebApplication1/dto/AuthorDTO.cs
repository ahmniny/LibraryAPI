using LibraryAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.dto
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Bio { get; set; }

    }
}
