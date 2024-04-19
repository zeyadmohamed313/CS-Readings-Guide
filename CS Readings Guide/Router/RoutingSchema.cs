using System.Security.Permissions;

namespace CS_Readings_Guide.Router
{
    public class RoutingSchema
    {
        public const string root = "Api";
        public const string version = "V1";
        public const string Rule = root + "/" + version + "/";

        public static class BookRouting
        {
            public const string Prefix = Rule + "Book";
            public const string GetAllBooks = Prefix + "/GetAllBooks";
            public const string GetBookById = Prefix + "/GetBookById/{Id}";
            public const string AddBook = Prefix + "/AddBook";
            public const string UpdateBook = Prefix + "/UpdateBook";
            public const string Delete = Prefix + "/DeleteBook/{Id}";
        }
        public static class CategoryRouting
        {
            public const string Prefix = Rule + "Category";
            public const string GetAllBooks = Prefix + "/GetAllCategories";
            public const string GetBookById = Prefix + "/GetCategoryById/{Id}";
            public const string AddBook = Prefix + "/AddCategory";
            public const string UpdateBook = Prefix + "/UpdateCategory";
            public const string Delete = Prefix + "/DeleteCategory/{Id}";
        }
        public static class AuthenticationRouting
        {
            public const string Prefix = Rule + "Authentication";
            public const string Register = Prefix + "/Register";
            public const string Login = Prefix + "/Login";
            public const string ChangePassword = Prefix + "/ChangePassword";  
        }

        public static class UserActivityRouting
        {
            public const string Prefix = Rule + "UserActivity";
            public const string GetFavouriteList = Prefix + "/GetFavouriteList";
            public const string GetCurrentlyReadingList = Prefix + "/GetCurrentlyReadingList";
            public const string GetReadList = Prefix + "/GetReadList";
            public const string GetToReadList = Prefix + "/GetToReadList";
            public const string AddBookToFavouriteList = Prefix + "/AddBookToFavouriteList/{Id}";
            public const string AddBookToCurrentlyReadingList = Prefix + "/AddBookToCurrentlyReadingList/{Id}";
            public const string AddBookToReadList = Prefix + "/AddBookToReadList/{Id}";
            public const string AddBookToToReadList = Prefix + "/AddBookTo ReadList/{Id}";


            public const string RemoveBookFromFavouriteList = Prefix + "/RemoveBookFromFavouriteList/{Id}";
            public const string RemoveBookFromCurrentlyReadingList = Prefix + "/RemoveBookFromCurrentlyReadingList/{Id}";
            public const string RemoveBookFromReadList = Prefix + "/RemoveBookFromReadList/{Id}";
            public const string RemoveBookFromToReadList = Prefix + "/RemoveBookFromToReadList/{Id}";

            public const string GetNotes = Prefix + "/GetNotes/{BookId}";
            public const string AddNote = Prefix + "/AddNote";
            public const string DeleteNote = Prefix + "/DeleteNote";

        }
    }
}
