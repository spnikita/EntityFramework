namespace EntityFramework.Models
{
    /// <summary>
    /// Книга
    /// </summary>
    internal class Book : Base
    {
        /// <summary>
        /// Название книги
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Год выпуска
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Пользователь книги
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Идентификатор жанра
        /// </summary>
        public int GenreId { get; set; }

        /// <summary>
        /// Жанр книги
        /// </summary>
        public required Genre Genre { get; set; }

        /// <summary>
        /// Авторы книги
        /// </summary>
        public required List<Author> Authors { get; set; }
    }
}
