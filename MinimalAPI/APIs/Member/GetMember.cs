using LibraryManagementSystem.API_Setup;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.APIs.Member
{
    public class GetMember : IWebApi
    {
        public void Register(WebApplication app)
        {
            app.MapGet("/GetMembers", async ([FromServices] IMemberRepository memberRepository) =>
            {
                var member = await memberRepository.GetMembers();
                return Results.Ok(member);
            })
        .WithMetadata(new EndpointNameMetadata("GetMembers"));
        }
    }


}
