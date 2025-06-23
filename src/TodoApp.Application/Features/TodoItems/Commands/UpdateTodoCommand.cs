using MediatR;
using AutoMapper;
using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces;
using TodoApp.Application.DTOs;

namespace TodoApp.Application.Features.TodoItems.Commands;

public class UpdateTodoCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
}

public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, bool>
{
    private readonly ITodoRepository _repository;

    public UpdateTodoCommandHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _repository.GetByIdAsync(request.Id);
        if (todo is null) return false;

        todo.Title = request.Title;
        todo.Description = request.Description;
        todo.IsCompleted = request.IsCompleted;

        await _repository.UpdateAsync(todo);
        return true;
    }
}