using Ticketera.Domain.Models.Entities;

namespace Ticketera.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<User> Users { get; }
    IGenericRepository<Ticket> Tickets { get; }
    Task<int> SaveChangesAsync();
}