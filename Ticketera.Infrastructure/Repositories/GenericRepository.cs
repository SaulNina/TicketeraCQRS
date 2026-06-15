using Microsoft.EntityFrameworkCore;
using Ticketera.Domain.Interfaces;
using Ticketera.Infrastructure.Models;

namespace Ticketera.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly TicketeraBdContext _context;
    internal DbSet<T> _dbSet;

    public GenericRepository(TicketeraBdContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
    public async Task<T> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id!);
    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
    public void Update(T entity) => _dbSet.Update(entity);
    public void Delete(T entity) => _dbSet.Remove(entity);
}