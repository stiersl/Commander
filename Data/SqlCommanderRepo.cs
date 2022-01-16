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

    public List<Command> GetAllCommands()
    {
      return _DBcontext.Commands.ToList();
    }

    public Command GetCommandById(int id)
    {
      return _DBcontext.Commands.FirstOrDefault(item => item.Id == id);
    }
  }
}