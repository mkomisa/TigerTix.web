using Data.Entities.Ticket.cs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using TigerTix.web.Data.Entities;

namespace TigerTix.web.Controllers
{
    public class TicketController : Controller {
    private readonly string _connectionString;
    public TicketController(IConfiguration configuration)
    {
        //get connection string from appsettings.json            
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    
    [Route("api/ticket")]
    [HttpGet]
    public IActionResult getTicketsByEventID(string eventID){
        List<Ticket> tickets= new List<Ticket>();
         using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var command = new SqlCommand("SELECT * FROM Tickets WHERE EventID=@eventID", connection);
            command.Parameters.AddWithValue("@EventID", "%" + eventID + "%");
            var reader = command.ExecuteReader();

             while (reader.Read())
                {
                    Ticket myTicket = new Ticket
                    {
                        EventId = (int)reader["EventId"],
                        ticketName = reader["TicketName"].ToString(),
                        price = (double)reader["Price"]
                    };
                    tickets.Add(myTicket);
                }
        }
        return Ok(tickets);
    } 
    }
}
