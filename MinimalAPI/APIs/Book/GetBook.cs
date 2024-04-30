using LibraryManagementSystem.API_Setup;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.APIs.Book
{
    public class GetBook : IWebApi
    {
        public void Register(WebApplication app)
        {
            app.MapGet("/GetBooks", async ([FromServices] IBookRepository bookRepository) =>
            {
                var book = await bookRepository.Getbooks();
                return Results.Ok(book);
            })
        .WithMetadata(new EndpointNameMetadata("GetBooks"));
        }
    }

}
