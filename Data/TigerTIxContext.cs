
using Microsoft.EntityFrameworkCore;
namespace TigerTix.web.Data.Entities;
public class TigerTixContext : DbContext
{
    public DbSet<User> users { get; set; }
}