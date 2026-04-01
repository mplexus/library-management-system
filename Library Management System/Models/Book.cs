using System.Text.Json.Serialization;

namespace Library_Management_System.Models
{
    public record Book(
        [property: JsonPropertyName("title")] string Title,
        [property: JsonPropertyName("genre")] string Genre,
        [property: JsonPropertyName("stock")] int Stock,
        [property: JsonPropertyName("cost")] int Cost) : IBook
    {
    }
}
