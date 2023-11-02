using Microsoft.EntityFrameworkCore;
using TacoMusings.API.Contexts;
using TacoMusings.API.Services;
using TacoMusings.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<TacoMusingsContext>(o => o.UseSqlServer(builder.Configuration["TacoMusingsLocal"]));
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IContentService, ContentService>();
builder.Services.AddScoped<ITagService, TagService>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
