using LibraryManagementSystem.API_Setup;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.APIs.BorrowedBook
{
    public class UpdateBorrowedBook : IWebApi
    {
        public void Register(WebApplication app)
        {
            app.MapPut("/UpdateBorrowedBook", async ([FromBody] Models.BorrowedBook borrowedBook, [FromServices] IBorrowedBookRepository borrowedBookRepository) =>
            {
                if (borrowedBook != null)
                {
                    await borrowedBookRepository.UpdateBorrowedBook(borrowedBook);
                    return Results.Ok();

                }
                return Results.NoContent();
            })
        .WithMetadata(new EndpointNameMetadata("UpdateBorrowedBook"));
        }
    }

}
