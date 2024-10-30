using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using TigerTix.web.Data.Entities;

namespace TigerTix.web.Controllers
{
    public class UserController : Controller
    {
        private readonly string _connectionString;

        public UserController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpPost]
        [Route("user/signup")]
        public IActionResult Signup(string firstName, string lastName, string username, string password)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest("All fields are required.");
            }

            // Create a new User instance
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Password = password
            };

            // Save the user to the SQL database
            SaveUserToDatabase(user);

            return Ok("User signed up successfully.");
        }

        private void SaveUserToDatabase(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Users (FirstName, LastName, Username, PasswordHash) VALUES (@FirstName, @LastName, @Username, @PasswordHash)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@PasswordHash", user.Password);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
