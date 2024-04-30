using LibraryManagementSystem.API_Setup;

using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace LibraryManagementSystem.APIs.Author
{
    public class UpdateAuthor : IWebApi
    {
        public void Register(WebApplication app)
        {
            app.MapPut("/UpdateAuthor", async ([FromBody] Models.Author author, [FromServices] IAuthorRepository authorRepository) =>
            {
                if (author != null)
                {
                    await authorRepository.UpdateAuthor(author);
                    return Results.Ok();

                }
                return Results.NoContent();
            })
        .WithMetadata(new EndpointNameMetadata("UpdateAuthor"));
        }
    }

}
