using EntityFramework.Models;

namespace EntityFramework.Interfaces
{
    /// <summary>
    /// Определяет методы работы с репозиторием пользователей
    /// </summary>
    internal interface IUserRepository : IGenericRepository<User>
    {
        /// <summary>
        /// Обновить имя пользователя по идентификатору пользователя
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="newUserName">Новое имя пользователя</param>
        /// <returns>Обновление прошло успешно, да/нет</returns>
        bool UpdateUserName(int id, string newUserName);

        /// <summary>
        /// Получить количество книг на руках у пользователя
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Количество книг на руках у пользователя</returns>
        int? GetUserNumberOfBooks(int id);

        /// <summary>
        /// Пользователь имеет на руках кингу, да/нет
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="bookId">Идентификатор книги</param>
        /// <returns>Пользователь имеет на руках кингу, да/нет</returns>
        bool? UserHasBook(int userId, int bookId);
    }
}
