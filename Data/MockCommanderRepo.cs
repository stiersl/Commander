using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
  public class MockCommanderRepo : ICommanderRepo
  {
    #region Variables
    private readonly List<Command> _commands;
    #endregion
    public MockCommanderRepo()
    {
      _commands = new List<Command> {
        new Command { Id = 0, HowTo = "create a directory", Line = "mkdir", Platform = "Linux" },
        new Command { Id = 1, HowTo = "Show all files in the directory", Line = "ls", Platform = "Linux" },
        new Command { Id = 2, HowTo = "Build docker containers", Line = "Docker-compose build", Platform = "Docker compose" }
      };
    }
    public List<Command> GetAllCommands()
    {
      return _commands;
    }

    public Command GetCommandById(int id)
    {
      return _commands.Find(item => item.Id == id);
    }
  }
}