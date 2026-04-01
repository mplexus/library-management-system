
namespace Library_Management_System.Models
{
    public record Book(string Title, string Genre, int Stock, int Cost) : IBook
    {
    }
}
