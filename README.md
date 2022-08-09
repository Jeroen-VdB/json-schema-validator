# Getting started

Validate a JSON file against a remote or local JSON Schema file:

```
dotnet build
dotnet run -s C:\some-file-path\schema.json -j C:\some-file-path\content.json
```

or

```
dotnet build
dotnet run  -s https://some-url.com/schema.json -j C:\some-file-path\content.json
```

Requires [.NET 6.0 or higher](https://dotnet.microsoft.com/en-us/download).
