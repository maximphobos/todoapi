using AutoMapper;
using ToDoListWebApi.Persistence.Models;
using ToDoListWebApi.ViewModels.ToDoListViewModels;

namespace ToDoListWebApi.Infrastructure.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ToDoTask, ToDoTaskViewModel>().ReverseMap();
        }
    }
}
