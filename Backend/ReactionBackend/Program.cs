using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5005");
var app = builder.Build();

app.MapPost("/api/reaction/events", async ([FromBody] JsonElement evt) =>
{
    string json = JsonSerializer.Serialize(evt, new JsonSerializerOptions { WriteIndented = true });
    await File.AppendAllTextAsync("events.log", json + Environment.NewLine);
    return Results.Ok(new { status = "event received" });
});

app.MapPost("/api/reaction/summaries", async ([FromBody] JsonElement summary) =>
{
    string json = JsonSerializer.Serialize(summary, new JsonSerializerOptions { WriteIndented = true });
    await File.AppendAllTextAsync("summaries.log", json + Environment.NewLine);
    return Results.Ok(new { status = "summary received" });
});

app.Run();