using Microsoft.EntityFrameworkCore;
using SimplAgent.Web.ApiService.Models.Documents;

namespace SimplAgent.Web.ApiService.Data;

public class AgentDbContext(DbContextOptions<AgentDbContext> options) : DbContext(options)
{
    public DbSet<Document> Documents { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
