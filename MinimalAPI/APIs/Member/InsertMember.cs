using LibraryManagementSystem.API_Setup;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.APIs.Member
{
    public class InsertMember : IWebApi
    {
        public void Register(WebApplication app)
        {
            app.MapPost("/InsertMember", async ([FromBody] Models.Member member,
                                            [FromServices] IMemberRepository memberRepository,
                                            [FromServices] LinkGenerator linkGenerator) =>
            {
                member.MemberID = 0;
                await memberRepository.InsertMember(member);
                return Results.Created($"/GetMemberByID/{member.MemberID}", member);

            })
        .WithMetadata(new EndpointNameMetadata("InsertMember"));
        }
    }

}
