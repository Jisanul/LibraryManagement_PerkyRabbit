using LibraryManagementSystem.API_Setup;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.APIs.Author
{
    public class GetAuthor : IWebApi
    {
        public void Register(WebApplication app)
        {
            app.MapGet("/GetAuthor", async ([FromServices] IAuthorRepository authorRepository) =>
            {
                var author = await authorRepository.GetAuthors();
                return Results.Ok(author);
            })
        .WithMetadata(new EndpointNameMetadata("GetAuthor"));
        }
    }

}
