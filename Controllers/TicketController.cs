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
        //implement this
        return Ok();
    } 

    }
}
