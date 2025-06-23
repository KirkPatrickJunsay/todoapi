using MediatR;
using AutoMapper;
using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces;
using TodoApp.Application.DTOs;

namespace TodoApp.Application.Features.TodoItems.Commands;

public class DeleteTodoCommand : IRequest<bool>
{
    public Guid Id { get; set; }

    public DeleteTodoCommand(Guid id)
    {
        Id = id;
    }
}

public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, bool>
{
    private readonly ITodoRepository _repository;

    public DeleteTodoCommandHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _repository.GetByIdAsync(request.Id);
        if (todo is null) return false;

        await _repository.DeleteAsync(request.Id);
        return true;
    }
}