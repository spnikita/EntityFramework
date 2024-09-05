using EntityFramework.Models;

namespace EntityFramework.Repositories
{
    internal sealed class AuthorRepository : GenericRepository<Author>
    {
        /// <summary>
        /// Параметризированный конструктор
        /// </summary>
        /// <param name="context"><inheritdoc cref="_context" path="/summary"/></param>
        public AuthorRepository(DigitalLibraryContext context) : base(context) { }
    }
}
