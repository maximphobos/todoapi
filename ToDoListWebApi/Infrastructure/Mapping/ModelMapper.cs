using AutoMapper;
using ToDoListWebApi.Persistence.Models;
using ToDoListWebApi.ViewModels.ToDoListViewModels;

namespace ToDoListWebApi.Infrastructure.Mapping;

public class ModelMapper : IModelMapper
{
    private readonly IMapper _mapper;

    public ModelMapper(IMapper mapper)
    {
        _mapper = mapper;
    }

    public T MapTo<T>(ToDoTaskViewModel model)
    {
        return MapObjectTo<T>(model);
    }

    public T MapTo<T>(ToDoTask model)
    {
        return MapObjectTo<T>(model);
    }

    public List<T> MapTo<T>(IList<ToDoTask> model)
    {
        return _mapper.Map<List<T>>(model);
    }

    private T MapObjectTo<T>(object model)
    {
        return _mapper.Map<T>(model);
    }
}
