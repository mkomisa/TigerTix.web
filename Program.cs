using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TigerTix.web.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add MVC services to the container
builder.Services.AddControllers();

var app = builder.Build();

// Serve static files from the wwwroot folder
app.UseStaticFiles();

// Enable routing for controllers
app.UseRouting();

// Map the root URL ("/") to serve the HomePage.html file
app.MapGet("/", async context =>
{
    context.Response.Redirect("/HomePage.html");
});

// Map controller routes
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
