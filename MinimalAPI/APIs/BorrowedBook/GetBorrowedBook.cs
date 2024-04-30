using LibraryManagementSystem.API_Setup;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.APIs.BorrowedBook
{
    public class GetBorrowedBook : IWebApi
    {
        public void Register(WebApplication app)
        {
            app.MapGet("/GetBorrowedBooks", async ([FromServices] IBorrowedBookRepository borrowedBookRepository) =>
            {
                var borrowedBook = await borrowedBookRepository.GetBorrowedBooks();
                return Results.Ok(borrowedBook);
            })
        .WithMetadata(new EndpointNameMetadata("GetBorrowedBooks"));
        }
    }

}
