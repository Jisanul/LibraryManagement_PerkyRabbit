using LibraryManagementSystem.API_Setup;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.APIs.Author
{
    public class GetAuthorByID : IWebApi
    {
        public void Register(WebApplication app)
        {
            app.MapGet("/GetAuthorByID", async (int id, [FromServices] IAuthorRepository authorRepository) =>
            {
                var person = await authorRepository.GetAuthorByID(id);
                return Results.Ok(person);
            })
        .WithMetadata(new EndpointNameMetadata("GetAuthorByID"));
        }
    }
}
