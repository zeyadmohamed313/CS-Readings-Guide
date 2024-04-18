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
    }
}
