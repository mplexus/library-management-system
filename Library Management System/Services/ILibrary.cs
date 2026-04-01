using Library_Management_System.Models;

namespace Library_Management_System.Services
{
    public interface ILibrary
    {
        public IEnumerable<IBook> GetAllBooks();

        public int GetAllBooksTotalvalue();

        public IEnumerable<IBook> GetBooksByGenre(string genre);

        public IEnumerable<IBook> GetBooksByTitle(string title);

        public IEnumerable<GenreCount> GetBooksOfGenreWithCount(string genre);

        public IEnumerable<IEnumerable<GenreCount>> GetBooksByGenreWithCount();

        public IEnumerable<BookByGenre> GetAllBooksByGenre();
    }
}
