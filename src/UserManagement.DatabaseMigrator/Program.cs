using DbUp;
using System;
using System.IO;
using System.Reflection;

namespace UserManagement.DatabaseMigrator
{
    class Program
    {
        static void Main(string[] args)
        {
            var schemaPath = Path.Combine(Directory.GetCurrentDirectory(), "Schema");

            var result = DeployChanges
                .To.PostgresqlDatabase(GetConnectionString())
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .WithTransaction()
                .Build()
                .PerformUpgrade();

            if(!result.Successful)
            {
                Console.Write($"============ An Error occured while applying migration, {result.Error.ToString()}");
            }
        }

        private static string GetConnectionString()
        {
            throw new NotImplementedException();
        }
    }
}
