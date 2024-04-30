using LibraryManagementSystem.API_Setup;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.APIs.BorrowedBook
{
    public class GetBorrowedBookByID : IWebApi
    {
        public void Register(WebApplication app)
        {
            app.MapGet("/GetBorrowedBookByID", async (int id, [FromServices] IBorrowedBookRepository borrowedBookRepository) =>
            {
                var person = await borrowedBookRepository.GetBorrowedBookByID(id);
                return Results.Ok(person);
            })
        .WithMetadata(new EndpointNameMetadata("GetBorrowedBookByID"));
        }
    }

}
