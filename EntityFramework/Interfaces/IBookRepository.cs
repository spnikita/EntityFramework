using EntityFramework.Models;

namespace EntityFramework.Interfaces
{
    /// <summary>
    /// Определяет методы работы с репозиторием книг
    /// </summary>
    internal interface IBookRepository : IGenericRepository<Book>
    {
        /// <summary>
        /// Обновить дату публикации книги по идентификатору книги
        /// </summary>
        /// <param name="id">Идентификатор книги</param>
        /// <param name="newReleaseDate">Новая дата публикации книги</param>
        /// <returns>Обновление прошло успешно, да/нет</returns>
        bool UpdateBookReleaseDate(int id, DateTime newReleaseDate);

        /// <summary>
        /// Получить список книг, отсортированный по названию
        /// </summary>
        /// <returns>Список книг, отсортированный по названию</returns>
        public IEnumerable<Book> GetAllOrderByName();

        /// <summary>
        /// Получить список книг, отсортированный по году выпуска в порядке убывания
        /// </summary>
        /// <returns>Список книг, отсортированный по году выпуска в порядке убывания</returns>
        public IEnumerable<Book> GetAllOrderByReleaseDateDesc();

        /// <summary>
        /// Получить последнюю вышедшую книгу
        /// </summary>
        /// <returns>Последняя вышедшая книга</returns>
        public Book? GetLastReleaseDateBook();

        /// <summary>
        /// Книга определенного автора с определенным названием есть в библиотеке, да/нет
        /// </summary>
        /// <param name="authorId">Идентификатор автора</param>
        /// <param name="bookName">Название книги</param>
        /// <returns>Книга определенного автора с определенным названием есть в библиотеке, да/нет</returns>
        public bool BookIsInTheLibrary(int authorId, string bookName);

        /// <summary>
        /// Получить количество книг определенного жанра
        /// </summary>
        /// <param name="genreId">Идентификатор жанра</param>
        /// <returns>Количество книг определенного жанра</returns>
        public int GetBookNumberByGenre(int genreId);

        /// <summary>
        /// Получить количество книг определенного автора
        /// </summary>
        /// <param name="genreId">Идентификатор автора</param>
        /// <returns>Количество книг определенного жанра</returns>
        public int GetBookNumberByAuthor(int authorId);

        /// <summary>
        /// Получить список книг определенного жанра и вышедших между определенными годами
        /// </summary>
        /// <param name="genreId">Идентификатор жанра</param>
        /// <param name="dateFrom">Левая граница даты издания книги</param>
        /// <param name="dateTo">Правая граница даты издания книги</param>
        /// <returns>Список книг определенного жанра и вышедших между определенными годами</returns>
        public IEnumerable<Book> GetBooksByGenreAndReleaseDate(int genreId, DateTime dateFrom, DateTime dateTo);
    }
}
