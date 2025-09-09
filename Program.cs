using Phi3PocApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddPhiModel(Path.Combine(builder.Environment.ContentRootPath, @"AIModel\cpu_and_mobile\cpu-int4-rtn-block-32-acc-level-4"));

var app = builder.Build();

app.UseHttpsRedirection();
app.RegisterApiEndpoints(builder.Configuration);

app.MapGet("/ping", () => {
    return Results.Ok("pong");
});

app.Run();
