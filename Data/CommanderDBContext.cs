using Microsoft.EntityFrameworkCore;
using Commander.Models;
namespace Commander.Data
{
    public class CommanderDBContext: DbContext
    {
        public CommanderDBContext(DbContextOptions<CommanderDBContext> opt) : base(opt)
        {
            
        }

// create a represenation of our Commands class in our database
// - we want a "Commands" table in our database that has the fields as defined
// in our "Command" class

        public DbSet<Command> Commands { get; set; }
    }
}