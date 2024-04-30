using LibraryManagementSystem.API_Setup;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.APIs.Author
{
    public class InsertAuthor : IWebApi
    {
        public void Register(WebApplication app)
        {
            app.MapPost("/InsertAuthor", async ([FromBody] Models.Author author,
                                            [FromServices] IAuthorRepository authorRepository,
                                            [FromServices] LinkGenerator linkGenerator) =>
            {
                author.AuthorID = 0;
                await authorRepository.InsertAuthor(author);
                return Results.Created($"/GetAuthorByID/{author.AuthorID}", author);

            })
        .WithMetadata(new EndpointNameMetadata("InsertAuthor"));
        }
    }

}
