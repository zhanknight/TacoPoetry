using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Threading.RateLimiting;
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

builder.Services.AddRateLimiter(opt =>
{
    opt.OnRejected = (context, _) =>
        {
        if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
            {
                context.HttpContext.Response.Headers.RetryAfter =
                    ((int)retryAfter.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo);
            }

        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.");

        return new ValueTask();
        };

    opt.AddSlidingWindowLimiter(policyName: "slidingWindow", options =>
        {
        options.PermitLimit = 5;
        options.Window = TimeSpan.FromSeconds(8);
        options.SegmentsPerWindow = 2;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 4;
        });
});

var app = builder.Build();

app.UseRateLimiter();

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
