using EntityFramework.Models;

namespace EntityFramework.Repositories
{
    internal sealed class GenreRepository : GenericRepository<Genre>
    {
        /// <summary>
        /// Параметризированный конструктор
        /// </summary>
        /// <param name="context"><inheritdoc cref="_context" path="/summary"/></param>
        public GenreRepository(DigitalLibraryContext context) : base(context) { }
    }
}
