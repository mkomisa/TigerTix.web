
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using TigerTix.web.Data.Entities;;
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Serve static files from the wwwroot folder
app.UseStaticFiles();

// Map the root URL ("/") to serve the HomePage.html file
app.MapGet("/", async context =>
{
    context.Response.Redirect("/HomePage.html");
});

app.Run();