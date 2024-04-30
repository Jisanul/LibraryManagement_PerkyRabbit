using LibraryManagementSystem.API_Setup;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.APIs.Book
{
    public class InsertBook : IWebApi
    {
        public void Register(WebApplication app)
        {
            app.MapPost("/InsertBook", async ([FromBody] Models.Book book,
                                            [FromServices] IBookRepository bookRepository,
                                            [FromServices] LinkGenerator linkGenerator) =>
            {
                book.BookID = 0;
                await bookRepository.InsertBook(book);
                return Results.Created($"/GetBookByID/{book.BookID}", book);

            })
        .WithMetadata(new EndpointNameMetadata("InsertBook"));
        }
    }

}
