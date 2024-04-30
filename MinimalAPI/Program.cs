using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.API_Setup;
using LibraryManagementSystem.DBContexts;
using LibraryManagementSystem.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddWebApi(typeof(Program));

var objBuilder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
IConfiguration conManager = objBuilder.Build();
var conn = conManager.GetConnectionString("LibraryManagementSystemDB");


builder.Services.AddDbContext<LibraryDBContext>(options =>
{
    options.UseSqlServer(conn);
});
builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IBorrowedBookRepository, BorrowedBookRepository>();
builder.Services.AddTransient<IMemberRepository, MemberRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

await app.RegisterWebApisAsync();
await app.RunAsync();
