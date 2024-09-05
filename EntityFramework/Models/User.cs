namespace EntityFramework.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    internal class User : Base
    {
        /// <summary>
        /// Имя
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Почта
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Книги пользователя
        /// </summary>
        public List<Book>? Books { get; set; }
    }
}
