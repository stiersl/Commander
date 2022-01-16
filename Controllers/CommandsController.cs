using System;
using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
    // mapper object - Maps
    private readonly IMapper _mapper;
    #endregion
    #region Constructor
    /* Constructor for this class. Setting itself up. here were pointing our repository to Interface repo. In the startup class we will indicate which class is actually used via dependancy injection. use dependay injection with repositories*/
    public CommandsController(ICommanderRepo repository, ILogger<CommandsController> logger, IMapper mapper)
    {
      _repository = repository;
      _logger = logger;
      _mapper = mapper;
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
    public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
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
        return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
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
    [HttpGet("{id}", Name="GetCommandById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
    public ActionResult<CommandReadDto> GetCommandById(int id)
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
        // map the data from the internal class to dto class
        return Ok(_mapper.Map<CommandReadDto>(commandItemFound));
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message);
        return Problem(ex.Message, "CommandsController", StatusCodes.Status417ExpectationFailed, "Error getting the Command from the database");
      }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //[ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
    public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
    {
      //_logger.LogInformation($"Starting CreateCommand; cmd={cmd.ToString}" );
      //use automapper to map from recieved commanddto to model 
      var commandModel = _mapper.Map<Command>(commandCreateDto);
      _repository.CreateCommand(commandModel);
      _repository.SaveChanges();

      var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

      // actually go and get the Command from DB
      return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);

      /* try
      {
        ;
        

        Command _commandItemCreated = _repository.CreateCommand(_mapper.Map<Command>(cmd));
        // check if a command was found
        if (_commandItemCreated == null)
        {
          //_logger.LogWarning($"Command Not Created= {0}.", cmd.ToString);
          return NotFound();
        }
        // all is cool return the user Found
        _logger.LogInformation("Command Created: {0}", _commandItemCreated.ToString());
        // map the data from the internal class to dto class
        return Ok(_mapper.Map<CommandReadDto>(_commandItemCreated));
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message);
        return Problem(ex.Message, "CommandsController", StatusCodes.Status417ExpectationFailed, "Error Creating Command In the Database");
      } */
    }
    // PUT api/commands/{id}
    [HttpPut("{id}")]
    public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
      {
        //_logger.LogInformation($"Starting CreateCommand; cmd={cmd.ToString}" );
        var commandModelFromRepo = _repository.GetCommandById(id);
        if (commandModelFromRepo == null)
        {
          return NotFound();
        }      
        //use automapper to map from recieved commanddto to model 
        _mapper.Map(commandUpdateDto,commandModelFromRepo);

        _repository.UpdateCommand(commandModelFromRepo);
        _repository.SaveChanges();

        return NoContent();
      }

      // Patch api/commands/{id}
    [HttpPatch("{id}")]
    public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
      {
        //_logger.LogInformation($"Starting CreateCommand; cmd={cmd.ToString}" );
        // get the command from the repository
        var commandModelFromRepo = _repository.GetCommandById(id);
        
        //see if it exists in our repo
        if (commandModelFromRepo == null)
        {
          return NotFound();
        }   
        //use automapper to map from recieved commanddto to model    
        var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
        //apply the patch
        patchDoc.ApplyTo(commandToPatch, ModelState);
        //validate theie
        if (!TryValidateModel(commandToPatch))
        {
          return ValidationProblem(ModelState);
        }

        //use automapper to map from commandToPath to CommandModelFromRepo
        _mapper.Map(commandToPatch,commandModelFromRepo);

        _repository.UpdateCommand(commandModelFromRepo);
        _repository.SaveChanges();

        return NoContent();
      }

        // DELETE api/commands/{id}
    [HttpDelete("{id}")]
    public ActionResult CommandDelete(int id)
      {
        //_logger.LogInformation($"Starting CreateCommand; cmd={cmd.ToString}" );
        // get the command from the repository
        var commandModelFromRepo = _repository.GetCommandById(id);
        
        //see if it exists in our repo
        if (commandModelFromRepo == null)
        {
          return NotFound();
        }
        _repository.DeleteCommand(commandModelFromRepo);
        _repository.SaveChanges();

        return NoContent();
      }

    #endregion
  }
}