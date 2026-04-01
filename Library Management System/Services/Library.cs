using Library_Management_System.Models;
using System.Linq;

namespace Library_Management_System.Services
{
    public class Library : ILibrary
    {
        private List<Book> _books;

        public Library()
        {
            _books = new();
            loadFixtured();
        }

        public IEnumerable<IBook> GetAllBooks()
        {
            return _books;
        }

        public int GetAllBooksTotalvalue()
        {
            return _books
                .Select(book => (book.Stock * book.Cost))
                .Sum(value => value)
                ;
        }

        public IEnumerable<IBook> GetBooksByGenre(string genre)
        {
            return _books.Where(book => book.Genre.ToLower() == genre.ToLower()).ToList();
        }

        public IEnumerable<IBook> GetBooksByTitle(string title)
        {
            return _books.Where(book => book.Title.ToLower().Contains(title.ToLower())).ToList();
        }

        public IEnumerable<GenreCount> GetBooksOfGenreWithCount(string genre)
        {
            return _books
                .Where(book => book.Genre == genre)
                .GroupBy(book => book.Genre, (key, book) => new GenreCount(key, book.Sum(b => b.Stock)))
                .ToList()
                ;
        }

        public IEnumerable<IEnumerable<GenreCount>> GetBooksByGenreWithCount()
        {
            return _books
                .GroupBy(book => book.Genre, (key, bookList) => new List<GenreCount> { new GenreCount(key, bookList.Sum(b => b.Stock)) })
                .ToList()
                ;
        }

        public IEnumerable<BookByGenre> GetAllBooksByGenre()
        {
            return _books
                .GroupBy(book => book.Genre, (key, bookList) => new BookByGenre(key, bookList))
                .ToList()
                ;
        }

        private void loadFixtured()
        {
            _books.Clear();

            _books.Add(new Book("1984", "Dystopian", 3, 12));
            _books.Add(new Book("Moby Dick", "Classic", 5, 15));
            _books.Add(new Book("The Time Machine", "SciFi", 2, 10));
            _books.Add(new Book("10000 yards below the sea", "SciFi", 4, 8));
        }
    }
}
