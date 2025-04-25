using Votely.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Votely.Infrastructure.Database;

public class VotelyDbContext : DbContext
{
    public VotelyDbContext(DbContextOptions<VotelyDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // here you can configure the model mappings between your .NET classes and the database tables.
    }

    public DbSet<Survey> Surveys { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Option> Options { get; set; }
}