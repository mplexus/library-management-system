using Library_Management_System.Models;
using Library_Management_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Library_Management_System;

public class LibraryManagementSystem
{
    private readonly ILogger<LibraryManagementSystem> _logger;
    private readonly ILibrary _library;

    public LibraryManagementSystem(ILogger<LibraryManagementSystem> logger, ILibrary library)
    {
        _logger = logger;
        _library = library;
    }

    [Function("GetBooks")]
    public async Task<IActionResult> GetBooks([HttpTrigger(AuthorizationLevel.Function, "get", Route = "books")] HttpRequest req, string genre)
    {
        return new OkObjectResult(_library.GetAllBooks());
    }

    [Function("GetBooksByGenreWithCount")]
    public async Task<IActionResult> GetBooksByGenreWithCount([HttpTrigger(AuthorizationLevel.Function, "get", Route = "books/count")] HttpRequest req)
    {
        return new OkObjectResult(_library.GetBooksByGenreWithCount());
    }

    [Function("GetBooksByGenre")]
    public async Task<IActionResult> GetBooksByGenre([HttpTrigger(AuthorizationLevel.Function, "get", Route = "books/genre/{genre}")] HttpRequest req, string genre)
    {
        return new OkObjectResult(_library.GetBooksByGenre(genre));
    }

    [Function("GetBooksOfGenreWithCount")]
    public async Task<IActionResult> GetBooksOfGenreWithCount([HttpTrigger(AuthorizationLevel.Function, "get", Route = "books/genre/{genre}/count")] HttpRequest req, string genre)
    {
        return new OkObjectResult(_library.GetBooksOfGenreWithCount(genre));
    }

    [Function("GetBooksTotalValue")]
    public async Task<IActionResult> GetBooksTotalValue([HttpTrigger(AuthorizationLevel.Function, "get", Route = "books/totalvalue")] HttpRequest req)
    {
        return new OkObjectResult(_library.GetAllBooksTotalvalue());
    }

    [Function("GetBooksByTitle")]
    public async Task<IActionResult> GetBooksByTitle([HttpTrigger(AuthorizationLevel.Function, "get", Route = "books/title/{title}")] HttpRequest req, string title)
    {
        return new OkObjectResult(_library.GetBooksByTitle(title));
    }

    [Function("GetAllBooksByGenre")]
    public async Task<IActionResult> GetAllBooksByGenre([HttpTrigger(AuthorizationLevel.Function, "get", Route = "books/genre")] HttpRequest req)
    {
        return new OkObjectResult(_library.GetAllBooksByGenre());
    }

    [Function("AddBook")]
    public async Task<IActionResult> AddBook([HttpTrigger(AuthorizationLevel.Function, "post", Route = "books")] HttpRequest req)
    {
        var book = await JsonSerializer.DeserializeAsync<Book>(req.Body);
        bool result = false;

        if (book != null)
        {
            result = _library.AddBook(book);
        }

        return new OkObjectResult(result ? "book added" : "failed to add book");
    }

    [Function("RemoveBook")]
    public async Task<IActionResult> RemoveBook([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "books/title/{title}")] HttpRequest req, string title)
    {
        bool result = false;
        var books = _library.GetBooksByTitle(title);
        if (books != null)
        {
            var book = books.FirstOrDefault() as Book;

            if (book != null)
            {
                result = _library.RemoveBook(book);
            }
        }

        return new OkObjectResult(result ? "book removed" : "failed to remove book");
    }
}