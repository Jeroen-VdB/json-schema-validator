using CommandLine;
using CommandLine.Text;

namespace JsonSchemaValidator
{
    internal class CommandLineOptions
    {
        [Option('s', "schema", Required = true, HelpText = "Local or remote path to th JSON Schema file to validate against.")]
        public string Schema { get; set; } = string.Empty;

        [Option('j', "json", Required = true, HelpText = "Local path to the JSON file that needs to be validated.")]
        public string Json { get; set; } = string.Empty;

        [Option('o', "output", HelpText = "Write the validation errors to a file (same file path as the provided JSON file with '.validated.json' appended).")]
        public bool Output { get; set; } = false;

        [Usage(ApplicationAlias = "Cegeka.Horizon.IPaaS.Azure.Agent.Console.exe")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                return new List<Example>() {
                    new Example("Local path",
                        new CommandLineOptions {
                            Schema = "C:\\some-file-path\\schema.json",
                            Json = "C:\\some-file-path\\content.json",
                            Output = true
                        }),
                    new Example("Remote path",
                        new CommandLineOptions {
                            Schema = "https://some-url.com/schema.json",
                            Json = "C:\\some-file-path\\content.json",
                        })
                };
            }
        }
    }
}
