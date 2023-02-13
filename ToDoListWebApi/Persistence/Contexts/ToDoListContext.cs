using Microsoft.EntityFrameworkCore;
using ToDoListWebApi.Persistence.Models;

namespace ToDoListWebApi.Persistence.Contexts;

public class ToDoListContext : DbContext
{
    public ToDoListContext(DbContextOptions<ToDoListContext> options)
    : base(options)
    {
    }

    public virtual DbSet<ToDoTask> ToDoTasks => Set<ToDoTask>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
