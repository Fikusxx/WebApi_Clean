using Clean.Application.Interfaces;
using Clean.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Clean.Persistence;

public sealed class NotesDbContext : DbContext, INotesDbContext
{
	public DbSet<Note> Notes { get; set; } = null!;

	public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options)
	{ }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}
