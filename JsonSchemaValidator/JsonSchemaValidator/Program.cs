using CommandLine;
using JsonSchemaValidator;

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (s, e) =>
{
    cts.Cancel();
    e.Cancel = true;
};

await Parser.Default.ParseArguments<CommandLineOptions>(args)
    .WithParsedAsync(Execute);

async Task Execute(CommandLineOptions options)
{
    var validator = new Validator();
    var errors = await validator.Validate(options.Schema, options.Json);

    if (errors.Any())
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        errors.ForEach(e => Console.WriteLine(e));
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Content is valid.");
    }

    Console.ForegroundColor = ConsoleColor.White;
}
