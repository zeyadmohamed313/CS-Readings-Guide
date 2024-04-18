using AutoMapper;
using Core.Dtos;
using Core.Entites;

namespace Services.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            BookMapping();
        }


        private void BookMapping()
        {
            CreateMap<Book, BookDto>().ReverseMap();
        }

    }
}
