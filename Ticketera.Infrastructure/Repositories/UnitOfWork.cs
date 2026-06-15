using Ticketera.Domain.Interfaces;
using Ticketera.Domain.Models.Entities;
using Ticketera.Infrastructure.Models;

namespace Ticketera.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly TicketeraBdContext _context;
    public IGenericRepository<User> Users { get; private set; }
    public IGenericRepository<Ticket> Tickets { get; private set; }

    public UnitOfWork(TicketeraBdContext context)
    {
        _context = context;
        Users = new GenericRepository<User>(_context);
        Tickets = new GenericRepository<Ticket>(_context);
    }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    public void Dispose() => _context.Dispose();
}