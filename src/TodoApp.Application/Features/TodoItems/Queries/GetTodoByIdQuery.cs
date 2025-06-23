using MediatR;
using AutoMapper;
using TodoApp.Core.Interfaces;
using TodoApp.Application.DTOs;

public class GetTodoByIdQuery : IRequest<TodoItemDto?>
{
    public Guid Id { get; set; }

    public GetTodoByIdQuery(Guid id)
    {
        Id = id;
    }
}

public class GetTodoByIdQueryHandler : IRequestHandler<GetTodoByIdQuery, TodoItemDto?>
{
    private readonly ITodoRepository _repository;
    private readonly IMapper _mapper;

    public GetTodoByIdQueryHandler(ITodoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TodoItemDto?> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
    {
        var todo = await _repository.GetByIdAsync(request.Id);
        return todo is null ? null : _mapper.Map<TodoItemDto>(todo);
    }
}
