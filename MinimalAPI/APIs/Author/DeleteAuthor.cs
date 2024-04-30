using LibraryManagementSystem.API_Setup;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.APIs.Author
{
    public class DeleteAuthor : IWebApi
    {
        public void Register(WebApplication app)
        {
            app.MapDelete("/DeleteAuthor/{id}", async (int id,
                                            [FromServices] IAuthorRepository authorRepository,
                                            [FromServices] LinkGenerator linkGenerator) =>
            {

                await authorRepository.DeleteAuthor(id);
                return Results.Ok();
            })
            .WithMetadata(new EndpointNameMetadata("DeleteAuthor"));
        }
    }
}
