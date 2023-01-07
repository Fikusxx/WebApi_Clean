using Clean.Domain;
using Microsoft.EntityFrameworkCore;

namespace Clean.Application.Interfaces;

public interface INotesDbContext
{
    public DbSet<Note> Notes { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken token);
}
