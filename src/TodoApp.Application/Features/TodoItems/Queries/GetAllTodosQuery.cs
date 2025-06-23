using MediatR;
using AutoMapper;
using TodoApp.Core.Interfaces;
using TodoApp.Application.DTOs;

namespace TodoApp.Application.Features.TodoItems.Queries;

public class GetAllTodosQuery : IRequest<List<TodoItemDto>> { }

public class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, List<TodoItemDto>>
{
    private readonly ITodoRepository _repository;
    private readonly IMapper _mapper;

    public GetAllTodosQueryHandler(ITodoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<TodoItemDto>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync();
        return _mapper.Map<List<TodoItemDto>>(items);
    }
}
