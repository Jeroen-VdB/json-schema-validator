using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace JsonSchemaValidator
{
    internal class Validator
    {
        private readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Validates a JSON file against a JSchema
        /// </summary>
        /// <param name="schema">JSchema object</param>
        /// <param name="jsonPath">Path to the local JSON file</param>
        /// <returns>List of error messages</returns>
        public List<string> Validate(JSchema schema, string jsonPath)
        {
            var errors = new List<string>();

            using var s = File.OpenText(jsonPath);
            using var reader = new JSchemaValidatingReader(new JsonTextReader(s));
            reader.Schema = schema;
            reader.ValidationEventHandler += (sender, args) => { errors.Add(args.Message); };
            while (reader.Read()) { }

            return errors;
        }

        /// <summary>
        /// Validates a JSON file against a JSON Schema remote or local file
        /// </summary>
        /// <param name="schemaPath">Path to the remote or local schema file</param>
        /// <param name="jsonPath">Path to the local JSON file</param>
        /// <returns>List of error messages</returns>
        public async Task<List<string>> Validate(string schemaPath, string jsonPath)
        {
            var schemaJson = await LoadSchema(schemaPath);
            var schema = JSchema.Parse(schemaJson);

            return Validate(schema, jsonPath);
        }

        private async Task<string> LoadSchema(string path)
            => path.StartsWith("http")
                    ? await _httpClient.GetStringAsync(path)
                    : File.ReadAllText(path);
    }
}
