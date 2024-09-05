using EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Repositories
{
    /// <summary>
    /// Обобщенный репозиторий
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        /// <summary>
        /// DB-контекст
        /// </summary>
        protected readonly DigitalLibraryContext _context;

        /// <summary>
        /// Параметризированный конструктор
        /// </summary>
        /// <param name="context"><inheritdoc cref="_context" path="/summary"/></param>
        protected GenericRepository(DigitalLibraryContext context) => _context = context;

        /// <inheritdoc />
        public void Add(in T entity) => _context.Set<T>().Add(entity);

        /// <inheritdoc />
        public IEnumerable<T> GetAll() => [.. _context.Set<T>()];

        /// <inheritdoc />
        public T? GetById(int id) => _context.Set<T>().Find(id);

        /// <inheritdoc />
        public bool Remove(int id)
        {
            var obj = GetById(id);
            if (obj is not null)
            {
                _context.Set<T>().Remove(obj);
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
