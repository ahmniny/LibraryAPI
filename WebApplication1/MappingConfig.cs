using AutoMapper;
using LibraryAPI.dto;
using LibraryAPI.Models;

namespace LibraryAPI
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Book,BookDTO>().ReverseMap();
            CreateMap<Author,AuthorDTO>().ReverseMap();
            CreateMap<Category,Category>().ReverseMap();
            CreateMap<MainCategory,MainCategory>().ReverseMap();
        }
    }
}
