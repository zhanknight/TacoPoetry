using Microsoft.EntityFrameworkCore;
using TacoPoetry.API.Contexts;
using TacoPoetry.API.Services;
using TacoPoetry.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<TacoPoetryContext>(o => o.UseSqlServer(builder.Configuration["TacoPoetryLocal"]));
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IContentService, ContentService>();
builder.Services.AddScoped<ITagService, TagService>();

builder.Services.AddHealthChecks();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapHealthChecks("/healthytaco");

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
