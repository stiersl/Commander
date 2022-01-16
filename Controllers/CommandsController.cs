using System;
using System.Collections.Generic;
using System.Net;
using Commander.Data;
using Commander.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Commander.Contollers
{
  // this decorator sets up this class to be http API controller
  [ApiController]
  /* this decorator specifies the default Route to our controller use the name of the class in front of the "Controller" Text Route here is /api/Commands */
  [Route("api/[controller]")]
  public class CommandsController : ControllerBase
  {
    #region Variables
    // points to the Commands repo
    private readonly ICommanderRepo _repository;
    // used to write log messages out
    private readonly ILogger<CommandsController> _logger;
    #endregion
    #region Constructor
    /* Constructor for this class. Setting itself up. here were pointing our repository to Interface repo. In the startup class we will indicate which class is actually used via dependancy injection. use dependay injection with repositories*/
    public CommandsController(ICommanderRepo repository, ILogger<CommandsController> logger)
    {
      _repository = repository;
      _logger = logger;
    }
    #endregion

    #region Methods
    /******************************************
     Set up the Rest API Endpoints
     ******************************************/
    /// <summary>
    /// REST Get API /api/commands - Returns all the commands
    /// This is a sychronous return
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<IEnumerable<Command>> GetAllCommands()
    {
      _logger.LogInformation("Starting GetAllCommands()");
      try
      {
        IEnumerable<Command> commandItems = _repository.GetAllCommands();
        // check if a command was found
        if (commandItems == null)
        {
          return NotFound();
        }
        //_logger.LogInformation("Sent {0} commands", commandItems.Count);
        return Ok(commandItems);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message);
        return Problem(ex.Message, "CommandsController", (int)HttpStatusCode.ExpectationFailed, "Error getting the Command from the database");
      }
    }
    /// <summary>
    /// API endpoint Returns the command based upon the id requested.
    /// This is a sychronous return
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
    public ActionResult<Command> GetCommandById(int id)
    {
      _logger.LogInformation($"Starting GetCommandById; id={id}");
      try
      {
        Command commandItemFound = _repository.GetCommandById(id);
        // check if a command was found
        if (commandItemFound == null)
        {
          _logger.LogWarning("commandItem id={0} not found.", id);
          return NotFound();
        }
        // all is cool return the user Found
        _logger.LogInformation("Command Found: {0}", commandItemFound.ToString());
        return Ok(commandItemFound);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message);
        return Problem(ex.Message, "CommandsController", StatusCodes.Status417ExpectationFailed, "Error getting the Command from the database");
      }
    }
    #endregion
  }
}