namespace EntityFramework.Models
{
    /// <summary>
    /// Жанр
    /// </summary>
    internal class Genre : Base
    {
        /// <summary>
        /// Наименование жанра
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Книги данного жанра
        /// </summary>
        public List<Book>? Books { get; set; }
    }
}
