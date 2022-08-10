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

        if (options.Output)
        {
            var outputPath = $"{Path.GetFileNameWithoutExtension(options.Json)}.validated.json";
            File.WriteAllLines(outputPath, errors);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Validation errors written to file.");
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Content is valid.");
    }

    Console.ForegroundColor = ConsoleColor.White;
}
