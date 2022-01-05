using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data{
  public class MockCommanderRepo : ICommanderRepo
  {
    public IEnumerable<Command> GetAppCommands()
    {
      var commands = new List<Command>
      {
        new Command{Id = 0, HowTo="Boil an Egg", Line="Boil Water", Platform="Kettle & Pan"},
        new Command{Id = 1, HowTo="Fry an Egg", Line="Oil pad add Egg", Platform="Kettle & Pan"},
        new Command{Id = 2, HowTo="Scramble an Egg", Line="oil pan and add mixed egg", Platform="Kettle & Pan"}
      };
      return commands;
    }

    public Command GetCommandById(int id)
    {
      return new Command{Id = 0, HowTo="Boil an Egg", Line="Boil Water", Platform="Kettle & Pan"};
    }
  }
}