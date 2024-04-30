using LibraryManagementSystem.API_Setup;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.APIs.Book
{
    public class GetBookByID : IWebApi
    {
        public void Register(WebApplication app)
        {
            app.MapGet("/GetBookByID", async (int id, [FromServices] IBookRepository bookRepository) =>
            {
                var person = await bookRepository.GetBookByID(id);
                return Results.Ok(person);
            })
        .WithMetadata(new EndpointNameMetadata("GetBookByID"));
        }
    }

}
