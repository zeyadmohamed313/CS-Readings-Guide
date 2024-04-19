using AutoMapper;
using Core.Dtos;
using Core.Entites;
using Core.Entites.Identity;

namespace Services.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            BookMapping();
            CategoryMapping();
            UserMapping();
            NoteMapping();
        }


        private void BookMapping()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Book, BookDtoWithOutId>().ReverseMap();

        }

        private void CategoryMapping()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryDtoWithOutId>().ReverseMap();
        }

        private void UserMapping()
        {
            CreateMap<AppUser,UserDto>().ReverseMap();
        }
        private void NoteMapping()
        {
            CreateMap<Note,NoteDto>().ReverseMap();
        }

    }
}
