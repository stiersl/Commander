using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles
{
  public class CommandsProfile : Profile
  {
    public CommandsProfile()
    {
      // this is going to crate a map between Command and CommandReadDTO
      // explictly declare your mappings
      CreateMap<Command, CommandReadDto>();
      CreateMap<CommandCreateDto, Command>();
      CreateMap<CommandUpdateDto, Command>();
      CreateMap<Command,CommandUpdateDto>();
    }
  }
}