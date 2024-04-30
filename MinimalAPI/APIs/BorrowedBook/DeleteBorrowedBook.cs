using LibraryManagementSystem.API_Setup;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.APIs.BorrowedBook
{
    public class DeleteBorrowedBook : IWebApi
    {
        public void Register(WebApplication app)
        {
            app.MapDelete("/DeleteBorrowedBook/{id}", async (int id,
                                            [FromServices] IBorrowedBookRepository borrowedBookRepository,
                                            [FromServices] LinkGenerator linkGenerator) =>
            {

                await borrowedBookRepository.DeleteBorrowedBook(id);
                return Results.Ok();
            })
            .WithMetadata(new EndpointNameMetadata("DeleteBorrowedBook"));
        }
    }

}
