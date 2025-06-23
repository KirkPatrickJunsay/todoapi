using AutoMapper;
using TodoApp.Application.DTOs;
using TodoApp.Core.Entities;

namespace TodoApp.Application.Mapping;

public class TodoProfile : Profile
{
    public TodoProfile()
    {
        CreateMap<TodoItem, TodoItemDto>().ReverseMap();
    }
}
