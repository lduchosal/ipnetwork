# IPNetwork Codebase Guidelines

## Build Commands
- Build solution: `dotnet build ./src/ipnetwork.sln`
- Build for release: `dotnet build -c release ./src/ipnetwork.sln`
- Build package: `dotnet pack -c release ./src/System.Net.IPNetwork`

## Test Commands
- Run all tests: `dotnet test ./src/TestProject/TestProject.csproj`
- Run single test: `dotnet test ./src/TestProject/TestProject.csproj --filter "FullyQualifiedName~TestMethodName"`
- Run with coverage: `dotnet test ./src/TestProject/TestProject.csproj --settings ./src/TestProject/coverlet.runsettings`

## Code Style
- Follow StyleCop rules in .editorconfig
- Interfaces must start with 'I' (e.g., `ICidrGuess`)
- Use PascalCase for class/method/property names
- No underscores for field names
- Use built-in type aliases (e.g., `int` not `Int32`)
- Place System directives before other using directives
- Prefer var when type is apparent, never for built-in types
- XML documentation required for public APIs
- 4-space indentation, CRLF line endings

## Error Handling
- Use standard exception types appropriately
- Validate inputs with clear error messages
- Return descriptive error codes or exceptions