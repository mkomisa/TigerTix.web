using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using TigerTix.web.Data.Entities;

namespace TigerTix.web.Controllers
{
public class EventManagersController : Controller{

    private readonly string _connectionString;
    public EventManagersController(IConfiguration configuration)
    {
        //get connection string from appsettings.json            
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    [HttpPost]
    [Route("eventmanager/signin")]
     public IActionResult Signin (string username, string password){
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Username and password are required.");
            }

            var user = ValidateCredentials(username, password);

            if (user == null)
            {
                return Redirect("/EventManagerLoginScreen.html?status=invalid");
            }

             // Redirect to HomePage.html with user information in query strings
            string fullName = $"{user.FirstName} {user.LastName}";
            return Redirect($"/HomePage.html?username={user.Username}&fullname={fullName}");

        }

    private User ValidateCredentials(string username, string password){
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM EventMangers WHERE Username = @Username";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string storedPassword = reader["Password"].ToString();

                        // For simplicity, we are doing a plain text comparison
                        // In real scenarios, passwords should be hashed and compared securely
                        if (password == storedPassword)
                        {
                            return new User
                            {
                                UserId = (int)reader["UserId"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Username = reader["Username"].ToString()
                                // other properties if needed
                            };
                        }
                    }
                }
            }
        }

        return null; // User not found or password doesn't match
    }
    }
}

    public 


}
}