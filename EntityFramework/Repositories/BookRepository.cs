using EntityFramework.Interfaces;
using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Repositories
{
    /// <summary>
    /// Репозиторий для работы с книгами
    /// </summary>
    internal sealed class BookRepository : GenericRepository<Book>, IBookRepository
    {
        /// <summary>
        /// Параметризированный конструктор
        /// </summary>
        /// <param name="context"><inheritdoc cref="_context" path="/summary"/></param>
        public BookRepository(DigitalLibraryContext context) : base(context) { }

        /// <inheritdoc />
        public bool UpdateBookReleaseDate(int id, DateTime newReleaseDate)
        {
            var book = GetById(id);
            if (book is not null)
            {
                book.ReleaseDate = newReleaseDate;
                _context.Entry(book).State = EntityState.Modified;

                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public IEnumerable<Book> GetAllOrderByName() => [.. _context.Set<Book>().OrderBy(x => x.Name)];

        /// <inheritdoc />
        public IEnumerable<Book> GetAllOrderByReleaseDateDesc() => [.. _context.Set<Book>().OrderByDescending(x => x.ReleaseDate)];

        /// <inheritdoc />
        public Book? GetLastReleaseDateBook() => _context.Set<Book>().OrderByDescending(x => x.ReleaseDate).FirstOrDefault();

        /// <inheritdoc />
        public bool BookIsInTheLibrary(int authorId, string bookName) => _context.Set<Book>().ToList().Any(book => book.Name.Equals(bookName, StringComparison.InvariantCultureIgnoreCase) && book.Authors.Any(author => author.Id == authorId));

        /// <inheritdoc />
        public int GetBookNumberByGenre(int genreId) => _context.Set<Book>().Where(el => el.GenreId == genreId).Count();

        /// <inheritdoc />
        public int GetBookNumberByAuthor(int authorId) => _context.Set<Book>().Where(el => el.Authors.Any(el => el.Id == authorId)).Count();

        /// <inheritdoc />
        public IEnumerable<Book> GetBooksByGenreAndReleaseDate(int genreId, DateTime dateFrom, DateTime dateTo) => [.. _context.Set<Book>().Where(el => el.GenreId == genreId && (dateFrom < el.ReleaseDate) && (el.ReleaseDate < dateTo))];
    }
}
