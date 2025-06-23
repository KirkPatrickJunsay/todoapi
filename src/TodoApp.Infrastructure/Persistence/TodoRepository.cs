using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces;

namespace TodoApp.Infrastructure.Persistence;

public class TodoRepository : ITodoRepository
{
    private readonly AppDbContext _context;

    public TodoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TodoItem item)
    {
        _context.TodoItems.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var item = await _context.TodoItems.FindAsync(id);
        if (item is not null)
        {
            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<TodoItem>> GetAllAsync()
    {
        return await _context.TodoItems.OrderByDescending(t => t.CreatedAt).ToListAsync();
    }

    public async Task<TodoItem?> GetByIdAsync(Guid id)
    {
        return await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task UpdateAsync(TodoItem item)
    {
        _context.TodoItems.Update(item);
        await _context.SaveChangesAsync();
    }
}
