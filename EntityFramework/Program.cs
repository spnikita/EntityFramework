using EntityFramework.Extensions;
using EntityFramework.Models;
using EntityFramework.Repositories;

namespace EntityFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"\tТестирование Entity Framework.");

            using var dbContext = new DigitalLibraryContext();

            var userRepository = new UserRepository(dbContext);

            var genreRepository = new GenreRepository(dbContext);

            var authorRepository = new AuthorRepository(dbContext);

            var bookRepository = new BookRepository(dbContext);

            #region Создание БД

            CreateDb(userRepository, genreRepository, authorRepository, bookRepository);

            #endregion            

            #region Работа с репозиторием пользователей            

            Console.WriteLine();
            Console.WriteLine("-------------------- Вызов методов работы с репозиторием пользователей --------------------");

            // Удалить несуществующего пользователя
            var nonExistentUserId = int.MaxValue;
            DeleteUser(userRepository, nonExistentUserId);

            // Удалить существующего пользователя
            var existingUserId = 4;
            DeleteUser(userRepository, existingUserId);

            // Вывести всех пользователей
            PrintAllUsers(userRepository);

            // Найти несуществующего пользователя
            FindUser(userRepository, nonExistentUserId);

            // Найти существующего пользователя
            existingUserId = 2;
            FindUser(userRepository, existingUserId);

            // Обновить имя несуществующего пользователя
            UpdateUserName(userRepository, nonExistentUserId, "Vasiliy");

            // Обновить имя существующего пользователя
            UpdateUserName(userRepository, existingUserId, "Vasiliy");

            // Вывести количество книг у каждого пользователя
            Console.WriteLine();
            PrintUserNumberOfBooks(userRepository, 1);
            PrintUserNumberOfBooks(userRepository, 2);
            PrintUserNumberOfBooks(userRepository, 3);
            PrintUserNumberOfBooks(userRepository, int.MaxValue);

            // Вывести, имеет ли указанный пользователь указанную книгу
            Console.WriteLine();
            PrintUserHasBook(userRepository, 1, 1);
            PrintUserHasBook(userRepository, 1, 2);
            PrintUserHasBook(userRepository, 2, 2);
            PrintUserHasBook(userRepository, 2, 3);
            PrintUserHasBook(userRepository, 3, 1);
            PrintUserHasBook(userRepository, int.MaxValue, 1);

            #endregion

            #region Работа с репозиторием жанров

            Console.WriteLine();
            Console.WriteLine("-------------------- Вызов методов работы с репозиторием жанров --------------------");

            PrintAllGenres(genreRepository);

            #endregion

            #region Работа с репозиторием авторов

            Console.WriteLine();
            Console.WriteLine("-------------------- Вызов методов работы с репозиторием авторов --------------------");

            PrintAllAuthors(authorRepository);

            #endregion

            #region Работа с репозиторием книг

            Console.WriteLine();
            Console.WriteLine("-------------------- Вызов методов работы с репозиторием книг --------------------");

            // Вывести список всех книг
            PrintAllBooks(bookRepository);

            // Вывести список всех книг, упорядоченных по имени
            PrintAllOrderByName(bookRepository);

            // Вывести список всех книг, упорядоченных по дате издания по убыванию
            PrintAllOrderByReleaseDateDesc(bookRepository);

            // Вывести последнюю вышедшую книгу
            PrintLastReleaseDateBook(bookRepository);

            // Вывести, есть ли в библиотеке книга указанного автора с определенным названием
            Console.WriteLine();
            PrintBookIsInTheLibrary(bookRepository, 2, "война и мир");
            PrintBookIsInTheLibrary(bookRepository, 2, "горе от ума");

            // Вывести, количество книг определенного жанра
            Console.WriteLine();
            PrintBookNumberByGenre(bookRepository, 1);
            PrintBookNumberByGenre(bookRepository, 2);
            PrintBookNumberByGenre(bookRepository, 3);
            PrintBookNumberByGenre(bookRepository, 4);
            PrintBookNumberByGenre(bookRepository, int.MaxValue);

            // Вывести, количество книг определенного автора
            Console.WriteLine();
            PrintBookNumberByAuthor(bookRepository, 1);
            PrintBookNumberByAuthor(bookRepository, 2);
            PrintBookNumberByAuthor(bookRepository, 3);
            PrintBookNumberByAuthor(bookRepository, 4);
            PrintBookNumberByAuthor(bookRepository, int.MaxValue);

            // Вывести список книг определенного жанра и вышедших между определенными годами
            PrintBooksByGenreAndReleaseDate(bookRepository, 1, new DateTime(1866, 01, 01), new DateTime(1868, 01, 01));

            #endregion

            Console.ReadKey();
        }

        /// <summary>
        /// Вывести последнюю вышедшую книгу
        /// </summary>
        /// <param name="repository">Репозиторий книг</param>        
        private static void PrintLastReleaseDateBook(BookRepository repository)
        {
            var book = repository.GetLastReleaseDateBook();

            if (book is null)
                return;

            Console.WriteLine("Последняя вышедшая книга:");
            Console.WriteLine($"→ идентификатор: {book.Id};");
            Console.WriteLine($"→ наименование: {book.Name};");
            Console.WriteLine($"→ год выпуска: {book.ReleaseDate:yyyy}.");
        }

        /// <summary>
        /// Вывести список всех книг, упорядоченных по дате издания по убыванию
        /// </summary>
        /// <param name="repository">Репозиторий книг</param>
        private static void PrintAllOrderByReleaseDateDesc(BookRepository repository)
        {
            Console.WriteLine();
            Console.WriteLine("Получение списка книг, упорядоченных по дате издания по убыванию.");

            var books = repository.GetAllOrderByReleaseDateDesc();
            Console.WriteLine("Всего книг: " + books.Count());

            if (!books.Any())
                return;

            var i = default(int);
            Console.WriteLine("Список книг:");
            foreach (var book in books)
            {
                Console.WriteLine($" Книга №{++i}:");
                Console.WriteLine($"→ идентификатор: {book.Id};");
                Console.WriteLine($"→ наименование: {book.Name};");
                Console.WriteLine($"→ год выпуска: {book.ReleaseDate:yyyy}.");
            }
        }

        /// <summary>
        /// Вывести список всех книг, упорядоченных по имени
        /// </summary>
        /// <param name="repository">Репозиторий книг</param>
        private static void PrintAllOrderByName(BookRepository repository)
        {
            Console.WriteLine();
            Console.WriteLine("Получение списка книг, упорядоченных по имени.");

            var books = repository.GetAllOrderByName();
            Console.WriteLine("Всего книг: " + books.Count());

            if (!books.Any())
                return;

            var i = default(int);
            Console.WriteLine("Список книг:");
            foreach (var book in books)
            {
                Console.WriteLine($" Книга №{++i}:");
                Console.WriteLine($"→ идентификатор: {book.Id};");
                Console.WriteLine($"→ наименование: {book.Name};");
                Console.WriteLine($"→ год выпуска: {book.ReleaseDate:yyyy}.");
            }
        }

        /// <summary>
        /// Вывести список книг определенного жанра и вышедших между определенными годами
        /// </summary>
        /// <param name="repository">Репозиторий книг</param>
        /// <param name="genreId">Идентификатор жанра</param>
        /// <param name="dateFrom">Левая граница даты издания книги</param>
        /// <param name="dateTo">Правая граница даты издания книги</param>
        private static void PrintBooksByGenreAndReleaseDate(BookRepository repository, int genreId, DateTime dateFrom, DateTime dateTo)
        {
            Console.WriteLine();
            Console.WriteLine($"Получение списка книг жанра с идентификатором {genreId} с датой издания между {dateFrom:yyyy} и {dateTo:yyyy}.");

            var books = repository.GetBooksByGenreAndReleaseDate(genreId, dateFrom, dateTo);
            Console.WriteLine("Всего книг: " + books.Count());

            if (!books.Any())
                return;

            var i = default(int);
            Console.WriteLine("Список книг:");
            foreach (var book in books)
            {
                Console.WriteLine($" Книга №{++i}:");
                Console.WriteLine($"→ идентификатор: {book.Id};");
                Console.WriteLine($"→ наименование: {book.Name};");
                Console.WriteLine($"→ год выпуска: {book.ReleaseDate:yyyy}.");
            }
        }

        /// <summary>
        /// Вывести количество книг определенного автора
        /// </summary>
        /// <param name="repository">Репозиторий книг</param>
        /// <param name="genreId">Идентификатор автора</param>     
        private static void PrintBookNumberByAuthor(BookRepository repository, int authorId)
        {
            var cnt = repository.GetBookNumberByAuthor(authorId);

            Console.WriteLine($"Количество книг автора с идентификатором №{authorId}: {cnt}");
        }

        /// <summary>
        /// Вывести количество книг определенного жанра
        /// </summary>
        /// <param name="repository">Репозиторий книг</param>
        /// <param name="genreId">Идентификатор жанра</param>        
        private static void PrintBookNumberByGenre(BookRepository repository, int genreId)
        {
            var cnt = repository.GetBookNumberByGenre(genreId);

            Console.WriteLine($"Количество книг жанра с идентификатором №{genreId}: {cnt}");
        }

        /// <summary>
        /// Вывести, есть ли в библиотеке книга указанного автора с определенным названием
        /// </summary>
        /// <param name="repository">Репозиторий книг</param>
        /// <param name="authorId">Идентификатор автора</param>
        /// <param name="bookName">Наименование книги</param>
        private static void PrintBookIsInTheLibrary(BookRepository repository, int authorId, string bookName)
        {
            var bookIsInTheLibrary = repository.BookIsInTheLibrary(authorId, bookName);

            Console.WriteLine($"Книга автора с идентификатором {authorId} и названием \"{bookName}\" есть в библиотеке: {bookIsInTheLibrary.ToYesNo()}");
        }

        /// <summary>
        /// Вывести, имеет ли указанный пользователь определенную книгу
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="bookId">Идентификатор книги</param>        
        private static void PrintUserHasBook(UserRepository userRepository, int userId, int bookId)
        {
            var userHasBook = userRepository.UserHasBook(userId, bookId);

            if (userHasBook is null)
                Console.WriteLine($"Отсутствует пользователь с идентификатором №{userId}");
            else
                Console.WriteLine($"Пользователь с идентификатором №{userId} имеет книгу с идентификатором №{bookId}: {userHasBook.Value.ToYesNo()}");
        }

        /// <summary>
        /// Вывести количество книг у пользователя
        /// </summary>
        /// <param name="repository">Репозиторий пользователей</param>
        /// <param name="userId">Идентификатор пользователя</param>
        private static void PrintUserNumberOfBooks(UserRepository repository, int userId)
        {
            var cnt = repository.GetUserNumberOfBooks(userId);

            if (cnt is null)
                Console.WriteLine($"Отсутствует пользователь с идентификатором №{userId}");
            else
                Console.WriteLine($"Количество книг у пользователя с идентификатором №{userId}: {cnt}");
        }

        /// <summary>
        /// Вывести все книги
        /// </summary>
        /// <param name="repository">Репозиторий книг</param>
        private static void PrintAllBooks(BookRepository repository)
        {
            Console.WriteLine();
            Console.WriteLine("Получение списка книг.");

            var books = repository.GetAll();
            Console.WriteLine("Всего книг: " + books.Count());

            if (!books.Any())
                return;

            var i = default(int);
            Console.WriteLine("Список книг:");
            foreach (var book in books)
            {
                Console.WriteLine($" Книга №{++i}:");
                Console.WriteLine($"→ идентификатор: {book.Id};");
                Console.WriteLine($"→ наименование: {book.Name};");
                Console.WriteLine($"→ год выпуска: {book.ReleaseDate:yyyy}.");
            }
        }

        /// <summary>
        /// Вывести всех авторов
        /// </summary>
        /// <param name="repository">Репозиторий авторов</param>
        private static void PrintAllAuthors(AuthorRepository repository)
        {
            Console.WriteLine();
            Console.WriteLine("Получение списка авторов.");

            var authors = repository.GetAll();
            Console.WriteLine("Всего авторов: " + authors.Count());

            if (!authors.Any())
                return;

            var i = default(int);
            Console.WriteLine("Список авторов:");
            foreach (var author in authors)
            {
                Console.WriteLine($" Автор №{++i}:");
                Console.WriteLine($"→ идентификатор: {author.Id};");
                Console.WriteLine($"→ ФИО: {author.Name};");
                Console.WriteLine($"→ дата рождения: {author.BirthData:yyyy-MM-dd}.");
            }
        }

        /// <summary>
        /// Вывести все жанры
        /// </summary>
        /// <param name="repository">Репозиторий жанров</param>
        private static void PrintAllGenres(GenreRepository repository)
        {
            Console.WriteLine();
            Console.WriteLine("Получение списка жанров.");

            var genres = repository.GetAll();
            Console.WriteLine("Всего жанров: " + genres.Count());

            if (!genres.Any())
                return;

            var i = default(int);
            Console.WriteLine("Список жанров:");
            foreach (var genre in genres)
            {
                Console.WriteLine($" Жанр №{++i}:");
                Console.WriteLine($"→ идентификатор: {genre.Id};");
                Console.WriteLine($"→ наименование: {genre.Name}.");
            }
        }

        /// <summary>
        /// Создать базу данных
        /// </summary>
        /// <param name="userRepository">Репозиторий пользователей</param>
        /// <param name="genreRepository">Репозиторий жанров</param>
        /// <param name="authorRepository">Репозиторий авторов</param>
        /// <param name="bookRepository">Репозиторий книг</param>
        private static void CreateDb(UserRepository userRepository, GenreRepository genreRepository, AuthorRepository authorRepository, BookRepository bookRepository)
        {
            Console.WriteLine();
            Console.WriteLine("Выполняется создание БД.");

            var user1 = new User { Name = "Arthur", Email = "Arthur@gmail.com" };
            var user2 = new User { Name = "Klim", Email = "Klim@yandex.ru" };
            var user3 = new User { Name = "Anna", Email = "Anna@yandex.ru" };
            var user4 = new User { Name = "Inna", Email = "Inna@gmail.ru" };

            var genre1 = new Genre { Name = "Роман-эпопея" };
            var genre2 = new Genre { Name = "Комедия" };
            var genre3 = new Genre { Name = "Поэма" };
            var genre4 = new Genre { Name = "Роман" };

            var author1 = new Author { Name = "Николай Васильевчи Гоголь", BirthData = new DateTime(1809, 04, 01) };
            var author2 = new Author { Name = "Лев Николаевич Толстой", BirthData = new DateTime(1828, 09, 09) };
            var author3 = new Author { Name = "Александр Сергеевич Грибоедов", BirthData = new DateTime(1795, 01, 15) };
            var author4 = new Author { Name = "Александр Сергеевич Пушкин", BirthData = new DateTime(1799, 06, 06) };

            var book1 = new Book { Name = "Война и мир", Genre = genre1, Authors = new List<Author>() { author2 }, User = user1, ReleaseDate = new DateTime(1867, 01, 01) };
            var book2 = new Book { Name = "Горе от ума", Genre = genre2, Authors = new List<Author>() { author3 }, User = user1, ReleaseDate = new DateTime(1825, 01, 01) };
            var book3 = new Book { Name = "Мертвые души", Genre = genre3, Authors = new List<Author>() { author1 }, User = user2, ReleaseDate = new DateTime(1842, 01, 01) };
            var book4 = new Book { Name = "Герой нашего времени", Genre = genre4, Authors = new List<Author>() { author4 }, ReleaseDate = new DateTime(1840, 01, 01) };

            userRepository.Add(user1);
            userRepository.Add(user2);
            userRepository.Add(user3);
            userRepository.Add(user4);

            genreRepository.Add(genre1);
            genreRepository.Add(genre2);
            genreRepository.Add(genre3);
            genreRepository.Add(genre4);

            authorRepository.Add(author1);
            authorRepository.Add(author2);
            authorRepository.Add(author3);
            authorRepository.Add(author4);

            bookRepository.Add(book1);
            bookRepository.Add(book2);
            bookRepository.Add(book3);
            bookRepository.Add(book4);

            bookRepository.Save();

            Console.WriteLine("Создание БД завершено.");
        }

        /// <summary>
        /// Обновить имя пользователя
        /// </summary>
        /// <param name="repository">Репозиторий пользователей</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="newUserName">Новое имя пользователя</param>
        private static void UpdateUserName(UserRepository repository, int userId, string newUserName)
        {
            Console.WriteLine();
            Console.WriteLine($"Обновление имени пользователя с идентификатором {userId}.");

            if (!repository.UpdateUserName(userId, newUserName))
            {
                Console.WriteLine("Пользователь с указанным идентификатором не найден.");
                return;
            }

            var rowsAffected = repository.Save();
            Console.WriteLine("Обновлено данных: " + rowsAffected);
        }

        /// <summary>
        /// Найти пользователя по идентификатору
        /// </summary>
        /// <param name="repository">Репозиторий пользователей</param>
        /// <param name="userId">Идентификатор пользователя</param>
        private static void FindUser(UserRepository repository, int userId)
        {
            Console.WriteLine();
            Console.WriteLine($"Поиск пользователя с идентификатором {userId}.");

            var user = repository.GetById(userId);
            if (user == null)
            {
                Console.WriteLine("Пользователь с указанным идентификатором не найден.");
                return;
            }

            Console.WriteLine("Пользователь найден:");
            Console.WriteLine($"→ идентификатор: {user.Id};");
            Console.WriteLine($"→ имя: {user.Name};");
            Console.WriteLine($"→ email: {user.Email}.");
            return;
        }

        /// <summary>
        /// Вывести всех пользователей
        /// </summary>
        /// <param name="repository">Репозиторий пользователей</param>
        private static void PrintAllUsers(UserRepository repository)
        {
            Console.WriteLine();
            Console.WriteLine("Получение списка пользователей.");

            var users = repository.GetAll();
            Console.WriteLine("Всего пользователей: " + users.Count());

            if (!users.Any())
                return;

            var i = default(int);
            Console.WriteLine("Список пользователей:");
            foreach (var user in users)
            {
                Console.WriteLine($" Пользователь №{++i}:");
                Console.WriteLine($"→ идентификатор: {user.Id};");
                Console.WriteLine($"→ имя: {user.Name};");
                Console.WriteLine($"→ email: {user.Email}.");
            }
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <param name="repository">Репозиторий пользователей</param>
        /// <param name="userId">Идентификатор пользователя</param>
        private static void DeleteUser(UserRepository repository, int userId)
        {
            Console.WriteLine();
            Console.WriteLine($"Удаление пользователя с идентификатором {userId}.");

            if (!repository.Remove(userId))
            {
                Console.WriteLine("Пользователь с указанным идентификатором не найден.");
                return;
            }

            var rowsAffected = repository.Save();
            Console.WriteLine("Удалено пользователей: " + rowsAffected);
        }
    }
}
