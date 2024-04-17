using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entites
{
    public class Book
    {
        public int Id {  get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public DateTime? PublishTime {  get; set; }
        public string? ImageUrl {  get; set; }
        public byte[]? Content { get; set; }
        public int? CategoryId {  get; set; }
        public Category Category { get; set; } // Like This Lazy Loading
        public List<Note> Notes { get; set; }
        public List<FavouriteList> Favourites { get; set; }
        public List<CurrentlyReadingList> CurrentlyReadingLists { get; set; }
        public List<ToReadList> toReadLists { get; set; }
        public List<ReadList> ReadLists { get; set; }


    }
}
