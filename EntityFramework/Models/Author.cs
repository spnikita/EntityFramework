namespace EntityFramework.Models
{
    /// <summary>
    /// Автор книги
    /// </summary>
    internal class Author : Base
    {
        /// <summary>
        /// Имя автора
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthData { get; set; }

        /// <summary>
        /// Книги данного автора
        /// </summary>
        public List<Book>? Books { get; set; }
    }
}
