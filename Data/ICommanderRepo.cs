using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{

  /* this interface will only define those methods that must exist on any class implementing this interface- this is basically a promise to all consumers that use this class, here is what i will return. the classes that impletment this interface will actually have to implement thete */
  public interface ICommanderRepo
  {
    bool SaveChanges();
    
    List<Command> GetAllCommands();
    Command GetCommandById(int id);
    void CreateCommand(Command cmd);
    void UpdateCommand(Command cmd);
    void DeleteCommand(Command cmd);
  }
}