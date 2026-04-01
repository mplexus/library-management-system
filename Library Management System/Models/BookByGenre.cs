
namespace Library_Management_System.Models
{
    public record BookByGenre(string Genre, IEnumerable<Book> Books)
    {
    }
}
