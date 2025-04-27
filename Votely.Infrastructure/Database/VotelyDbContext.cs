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

    public DbSet<SurveyModel> Surveys { get; set; }
    public DbSet<QuestionModel> Questions { get; set; }
    public DbSet<OptionModel> Options { get; set; }
}