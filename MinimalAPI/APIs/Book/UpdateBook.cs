using LibraryManagementSystem.API_Setup;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.APIs.Book
{
    public class UpdateBook : IWebApi
    {
        public void Register(WebApplication app)
        {
            app.MapPut("/UpdateBook", async ([FromBody] Models.Book book, [FromServices] IBookRepository bookRepository) =>
            {
                if (book != null)
                {
                    await bookRepository.UpdateBook(book);
                    return Results.Ok();

                }
                return Results.NoContent();
            })
        .WithMetadata(new EndpointNameMetadata("UpdateBook"));
        }
    }

}
