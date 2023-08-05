namespace BookStoreApi.Models
{
    public class BookStoreDatabaseSettings
    {
        public string BooksCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
