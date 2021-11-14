using DbUp;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.IO;

namespace UserManagement.DatabaseMigrator
{
    class Program
    {
        static void Main(string[] args)
        {
            var schemaPath = Path.Combine(Directory.GetCurrentDirectory(), "Schema");
            var result = DeployChanges
                .To.PostgresqlDatabase(GetConnectionString())
                .WithScriptsFromFileSystem(schemaPath)
                .LogToConsole()
                .WithTransaction()
                .Build()
                .PerformUpgrade();

            if (!result.Successful)
            {
                Console.Write($"============ An Error occured while applying migration, {result.Error.ToString()}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success!");
                Console.ResetColor();
            }

            Console.ReadLine();
        }

        private static string GetConnectionString()
        {
            var configuration = BuildConfiguration();

            var connectionStringBuilder = new NpgsqlConnectionStringBuilder
            {
                Host = configuration["POSTGRES_HOST"],
                Port = int.Parse(configuration["POSTGRES_PORT"] ?? "5432"),
                SslMode = SslMode.Prefer,
                Username = configuration["POSTGRES_USERNAME"],
                Password = configuration["POSTGRES_PASSWORD"],
                Database = configuration["POSTGRES_DB_NAME"],
                TrustServerCertificate = true
            };

            return connectionStringBuilder.ConnectionString;
        }

        private static IConfiguration BuildConfiguration() =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables()
                .Build();
    }
}
