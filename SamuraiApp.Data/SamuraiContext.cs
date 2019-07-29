using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;


namespace SamuraiApp.Data
{
    public class SamuraiContext : DbContext
    {
        private const string ConnectionString = "Server = (localdb)\\mssqllocaldb; Database = SamuraiAppData; Trusted_Connection = True;";
        public static readonly LoggerFactory MyConsoleLoggerFactory = new LoggerFactory(
            new[] {
                new ConsoleLoggerProvider((category, level) => category == DbLoggerCategory.Database.Command.Name
                  && level == LogLevel.Information, true)
            }
            );
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder
                  .UseLoggerFactory(MyConsoleLoggerFactory)
                  .EnableSensitiveDataLogging(true)
                  .UseSqlServer(ConnectionString, options => options.MaxBatchSize(150)); // Added data provider, configuring max size of statments in a Batch
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>()
                .HasKey(s => new { s.SamuraiId, s.BattleId }); // define composite key in associated table SamuraiBattle
        }
    }
}
