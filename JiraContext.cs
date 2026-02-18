using jira_clone_backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class JiraContextFactory : IDesignTimeDbContextFactory<JiraContext>
{
    public JiraContext CreateDbContext(string[] args)
    {
        Console.WriteLine("Factory is being called!");

        // Load .env file at design time
        DotNetEnv.Env.Load();

        var connectionString = Environment.GetEnvironmentVariable("PostgresConnectionString");
        Console.WriteLine($"Original connection string: {connectionString}");

        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentNullException("PostgresConnectionString is missing from .env");

        // Convert postgres:// URI to Npgsql format
        var npgsqlConnectionString = ConvertPostgresUriToNpgsql(connectionString);
        Console.WriteLine($"Converted connection string: {npgsqlConnectionString}");

        var optionsBuilder = new DbContextOptionsBuilder<JiraContext>();
        optionsBuilder.UseNpgsql(npgsqlConnectionString);

        return new JiraContext(optionsBuilder.Options);
    }

    private string ConvertPostgresUriToNpgsql(string postgresUri)
    {
        var uri = new Uri(postgresUri);

        var host = uri.Host;
        var port = uri.Port;
        var database = uri.AbsolutePath.TrimStart('/');
        var username = uri.UserInfo.Split(':')[0];
        var password = uri.UserInfo.Split(':')[1];

        // No SSL settings
        return $"Host={host};Port={port};Database={database};Username={username};Password={password}";
    }
}