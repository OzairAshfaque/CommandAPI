using AutoMapper;
using CommandAPI.Dtos;
using CommandAPI.Models;

namespace CommandAPI.Profiles
{
    public class CommandProfile : Profile
    {
        public CommandProfile()
        {
            //Source ➤ Target
            CreateMap<Command, CommandReadDto>();

            //Source ➤ Target
            CreateMap<CommandCreateDto, Command>();
            
            //Source ➤ Target
            CreateMap<CommandUpdateDto, Command>();

            //Source ➤ Target
            CreateMap<Command, CommandUpdateDto>();
            
        }
    }
}