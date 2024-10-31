using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using TigerTix.web.Data.Entities;

namespace TigerTix.web.Controllers
{
[Route("/api/events")]
public class EventController : ControllerBase{
    private string _connectionString;
    public EventController(IConfiguration configuration)
    {
        //get connection string from appsettings.json            
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    [HttpGet]
    public IActionResult Get(){
        var events= new List<Event>();

        using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Events", connection);
                var reader = command.ExecuteReader();
                Event myevent =new Event();

                while(reader.Read()){
                    myevent.EventId = (int)reader["EventId"];
                    myevent.Title = reader["Title"].ToString();
                    myevent.Date = reader["Date"].ToString();
                    myevent.Time = reader["Time"].ToString();
                    myevent.Location = reader["Location"].ToString();
                    events.Add(myevent);
                }

    }
    return Ok(events);

}
}
}


