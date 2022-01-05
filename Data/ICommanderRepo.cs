using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{

/* this interface will only define those methods that must exist on any class implementing this interface- this is basically a promise to all consumers that they
can use this methods to get the #pragma warning disable format */
  public interface ICommanderRepo
  {
    IEnumerable<Command> GetAppCommands();
    Command GetCommandById(int id);
  }
}