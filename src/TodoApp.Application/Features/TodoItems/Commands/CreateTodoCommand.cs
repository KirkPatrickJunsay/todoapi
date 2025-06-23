using MediatR;
using AutoMapper;
using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces;
using TodoApp.Application.DTOs;

namespace TodoApp.Application.Features.TodoItems.Commands;

public class CreateTodoCommand : IRequest<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Guid>
{
    private readonly ITodoRepository _repository;
    private readonly IMapper _mapper;

    public CreateTodoCommandHandler(ITodoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = new TodoItem
        {
            Title = request.Title,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(todo);
        return todo.Id;
    }
}
