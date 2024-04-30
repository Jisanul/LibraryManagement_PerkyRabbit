using LibraryManagementSystem.API_Setup;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.APIs.BorrowedBook
{
    public class InsertBorrowedBook : IWebApi
    {
        public void Register(WebApplication app)
        {
            app.MapPost("/InsertBorrowedBook", async ([FromBody] Models.BorrowedBook borrowedBook,
                                            [FromServices] IBorrowedBookRepository borrowedBookRepository,
                                            [FromServices] LinkGenerator linkGenerator) =>
            {
                borrowedBook.BorrowedBookID = 0;
                await borrowedBookRepository.InsertBorrowedBook(borrowedBook);
                return Results.Created($"/GetBorrowedBookByID/{borrowedBook.BorrowedBookID}", borrowedBook);

            })
        .WithMetadata(new EndpointNameMetadata("InsertBorrowedBook"));
        }
    }

}
