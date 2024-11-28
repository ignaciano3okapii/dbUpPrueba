using System.Reflection;
using DbUp;

var connectionString =
    args.FirstOrDefault()
    ?? "Server=DESKTOP-HE63B6D; Database=MyApp; Trusted_connection=true; TrustServerCertificate=True";

// Lo que hace esto es q si no existe la base la crea
EnsureDatabase.For.SqlDatabase(connectionString);

// Las migraciones estan en /scripts
var upgrader =
    DeployChanges.To
        .SqlDatabase(connectionString)
        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
        .LogToConsole()
        .Build();

var result = upgrader.PerformUpgrade();

if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result.Error);
    Console.ResetColor();
#if DEBUG
    Console.ReadLine();
#endif
}
else
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Success!");
    Console.ResetColor();
}

