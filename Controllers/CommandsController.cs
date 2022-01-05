using System.Collections.Generic;
using Commander.Data;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Contollers
{
  // decorate our class 
  // speficy the default Route to our controllers
  // use the name of the class in front of the "Controller" Text
  [Route("api/[controller]")]
  // or hard code it [Route("api/commands"]
  [ApiController]
  public class CommandsController : ControllerBase
  {
    private readonly ICommanderRepo _repository;

    // create a constructor here gett8hg the repostiory here we need
    // use the interface class. note in startup class you will see that
    // the Mock CommanderRepo class is used. 
    public CommandsController(ICommanderRepo repository)
    {
        _repository = repository;
    }

    // this endpoint will be called whenever anyone  makes a get request to api/conmands
    [HttpGet]
    public ActionResult <IEnumerable<Command>> GetAllCommands()
    {
      var commandItems = _repository.GetAppCommands();
      return Ok(commandItems);

    }
    // this endpoint will be made whenever anyone makes a get request to api/commands/5 where 5 = id of commands
    [HttpGet("{id}")]
    public ActionResult <Command> GetCommandById (int id)
    {
      var commandItem = _repository.GetCommandById(id);
      return Ok(commandItem);
    }
    
  }
  
}