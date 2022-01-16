using System;
using System.Collections.Generic;
using System.Linq;
using Commander.Models;

namespace Commander.Data
{


  public class SqlCommanderRepo: ICommanderRepo
    {
    private readonly CommanderDBContext _DBcontext;

    public SqlCommanderRepo(CommanderDBContext DBcontext)
        {
        _DBcontext = DBcontext;
        }

    public void CreateCommand(Command cmd)
    {
      if (cmd == null)
      {
        throw new ArgumentNullException(nameof(cmd));
      }
      // add the command to the db
      _DBcontext.Commands.Add(cmd);
    }

    public void DeleteCommand(Command cmd)
    {
      if (cmd == null)
      {
        throw new ArgumentNullException(nameof(cmd));
      }
      // add the command to the db
      _DBcontext.Commands.Remove(cmd);
    }

    public List<Command> GetAllCommands()
    {
      return _DBcontext.Commands.ToList();
    }

    public Command GetCommandById(int id)
    {
      return _DBcontext.Commands.FirstOrDefault(item => item.Id == id);
    }

    public bool SaveChanges()
    {
      return (_DBcontext.SaveChanges() >= 0);
    }

    public void UpdateCommand(Command cmd)
    {
      //nothing.
    }
  }
}