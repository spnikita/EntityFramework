using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework
{
    /// <summary>
    /// DB-контекст электронной библиотеки
    /// </summary>
    internal class DigitalLibraryContext : DbContext
    {
        /// <summary>
        /// Список пользователей
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Список книг
        /// </summary>
        public DbSet<Book> Books { get; set; }

        /// <summary>
        /// Список жанров
        /// </summary>
        public DbSet<Genre> Genres { get; set; }

        /// <summary>
        /// Список авторов
        /// </summary>
        public DbSet<Author> Authors { get; set; }

        public DigitalLibraryContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        /// <inheritdoc />       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS01;Database=entity_framework;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
