using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using TigerTix.web.Data.Entities;

namespace TigerTix.web.Controllers
{
[Route("/api/events")]
public class EventController : Controller{
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
               
                
                while(reader.Read()){
                     Event myevent = new Event();
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
    //putting in Http attribute makes it a child route
    [HttpGet("search")]
    public IActionResult GetEventsByTitle(string title){
         var events = new List<Event>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                //"LIKE" is used in SQL command to get events simiar not exact incase of spelling errors
                var command = new SqlCommand("SELECT * FROM Events WHERE Title LIKE @Title", connection);
                command.Parameters.AddWithValue("@Title", "%" + title + "%");
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Event myevent = new Event
                    {
                        EventId = (int)reader["EventId"],
                        Title = reader["Title"].ToString(),
                        Date = reader["Date"].ToString(),
                        Time = reader["Time"].ToString(),
                        Location = reader["Location"].ToString()
                    };
                    events.Add(myevent);
                }
            }

            return Ok(events);
        }


    [HttpPost]
    public IActionResult Post([FromBody]Event newEvent )
    {
        using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO events (Title, Date, Time, Location) VALUES (@Title, @Date, @Time, @Location)", connection);
                command.Parameters.AddWithValue("@Title", newEvent.Title);
                command.Parameters.AddWithValue("@Date", newEvent.Date);
                command.Parameters.AddWithValue("@Time", newEvent.Time);
                command.Parameters.AddWithValue("@Location", newEvent.Location);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok("Event added successfully");
                }
                else
                {
                    return StatusCode(500, "An error occurred while adding the event");
                }
            }
        }

                


    [HttpPost("delete")]
    public IActionResult DeleteEvent(int EventID){
        using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Events WHERE EventId=@EventID", connection);
                command.Parameters.AddWithValue("@EventID",EventID);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok("Event deleted successfully");
                }
                else
                {
                    return StatusCode(500, "An error occurred while deleting the event or EventID doesn't exist");
                }

    }
    }
}
}


