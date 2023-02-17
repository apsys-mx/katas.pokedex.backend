using FluentMigrator.Runner;
using katas.pokedex;
using katas.pokedex.migrations;
using Microsoft.Extensions.DependencyInjection;

try
{
    CommandLineArgs parameter = new CommandLineArgs();
    if (!parameter.ContainsKey("cnn"))
        throw new ArgumentException("No [cnn] parameter received. You need pass the connection string in order to execute the migrations");

    bool isRollBack = parameter.ContainsKey("rollback");

    string connectionString = parameter["cnn"];
    var serviceProvider = CreateServices(connectionString);
    using (var scope = serviceProvider.CreateScope())
    {
        if (isRollBack)
        {
            long rollBackToVersion = 0;
            if (parameter["rollback"].ToLower().Trim() == "one")
            {
                var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
                var lastMigration = runner.MigrationLoader.LoadMigrations().LastOrDefault();
                rollBackToVersion = lastMigration.Value.Version - 1;
            }
            else if (!long.TryParse(parameter["rollback"], out rollBackToVersion))
                throw new ArgumentException($"Invalid rollback version value: [{parameter["rollback"]}]");

            // Execute rollback
            RollbackDatabase(scope.ServiceProvider, rollBackToVersion);
        }
        else
            UpdateDatabase(scope.ServiceProvider);
    }
    return (int)ExitCode.Success;
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Error updating the database schema: {ex.Message}");
    Console.ResetColor();
    return (int)ExitCode.UnknownError;
}

/// <summary>
/// Configure the dependency injection services
/// </sumamry>
static IServiceProvider CreateServices(string connectionString)
{
    return new ServiceCollection()
        .AddFluentMigratorCore()
        .ConfigureRunner(rb => rb
            .AddSqlServer2016()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(typeof(M001_CreatePokemonsTable).Assembly).For.Migrations())
        .AddLogging(lb => lb.AddFluentMigratorConsole())
        .BuildServiceProvider(false);
}

static void UpdateDatabase(IServiceProvider serviceProvider)
{
    var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

static void RollbackDatabase(IServiceProvider serviceProvider, long rollbackVersion)
{
    var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateDown(rollbackVersion);
}

/// <summary>
/// Enumerate the exit codes
/// </summary>
enum ExitCode
{
    Success = 0,
    UnknownError = 1
}