using LibraryManagementSystem.API_Setup;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.APIs.Book
{
    public class DeleteBook : IWebApi
    {
        public void Register(WebApplication app)
        {
            app.MapDelete("/DeleteBook/{id}", async ([FromBody] int id,
                                            [FromServices] IBookRepository bookRepository,
                                            [FromServices] LinkGenerator linkGenerator) =>
            {

                await bookRepository.DeleteBook(id);
                return Results.Ok();
            })
            .WithMetadata(new EndpointNameMetadata("DeleteBook"));
        }
    }


}
