using EntityFramework.Interfaces;
using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace EntityFramework.Repositories
{
    /// <summary>
    /// Репозиторий для работы с пользователями
    /// </summary>
    internal sealed class UserRepository : GenericRepository<User>, IUserRepository
    {
        /// <summary>
        /// Параметризированный конструктор
        /// </summary>
        /// <param name="context"><inheritdoc cref="_context" path="/summary"/></param>
        public UserRepository(DigitalLibraryContext context) : base(context) { }

        /// <inheritdoc />
        public bool UpdateUserName(int id, string newUserName)
        {
            var user = GetById(id);
            if (user is not null)
            {
                user.Name = newUserName;
                _context.Entry(user).State = EntityState.Modified;

                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public int? GetUserNumberOfBooks(int id)
        {
            var user = GetById(id);
            if (user is null)
                return default;

            return user.Books?.Count ?? default;
        }

        /// <inheritdoc />
        public bool? UserHasBook(int userId, int bookId)
        {
            var user = GetById(userId);
            if (user is null)
                return default;

            return user.Books?.Any(el => el.Id == bookId) ?? default;
        }
    }
}
