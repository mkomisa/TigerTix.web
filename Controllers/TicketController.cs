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
        var tickets= new List<Ticket>();
         using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var command = new SqlCommand("SELECT * FROM Tickets WHERE EventId=@EventId", connection);
            command.Parameters.AddWithValue("@EventId", int.Parse(eventID));
            var reader = command.ExecuteReader();
             while (reader.Read())
                {
                     for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.WriteLine($"{reader.GetName(i)}: {reader[i]}");
            }

                    Ticket myTicket = new Ticket
                    {
                        ticketID=(int)reader["TicketId"],
                        EventId = (int)reader["EventId"],
                        ticketName = reader["TicketName"].ToString(),
                        price = Convert.ToDouble(reader["Price"])
                    };
    
                    tickets.Add(myTicket);
                    Console.WriteLine($"Added Ticket: {tickets[0]}");
                }
        return Ok(tickets);
        }
    }
    }
}
