# LogicBuilder.App.Bsl.Business

[![CI](https://github.com/BpsLogicBuilder/LogicBuilder.App.Bsl.Business/actions/workflows/ci.yml/badge.svg)](https://github.com/BpsLogicBuilder/LogicBuilder.App.Bsl.Business/actions/workflows/ci.yml)
[![CodeQL](https://github.com/BpsLogicBuilder/LogicBuilder.App.Bsl.Business/actions/workflows/github-code-scanning/codeql/badge.svg)](https://github.com/BpsLogicBuilder/LogicBuilder.App.Bsl.Business/actions/workflows/github-code-scanning/codeql)
[![codecov](https://codecov.io/github/BpsLogicBuilder/LogicBuilder.App.Bsl.Business/graph/badge.svg?token=AR2BPMR4TA)](https://codecov.io/github/BpsLogicBuilder/LogicBuilder.App.Bsl.Business)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=BpsLogicBuilder_LogicBuilder.App.Bsl.Business&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=BpsLogicBuilder_LogicBuilder.App.Bsl.Business)

## Overview

`LogicBuilder.App.Bsl.Business` is a .NET Standard 2.0 library that provides the foundational request and response data structures for LogicBuilder business services. This library defines the contract between clients and LogicBuilder business service implementations, enabling strongly-typed, serializable communication for CRUD operations and complex queries.

## Purpose

This library serves as the communication protocol layer for LogicBuilder-based applications, providing:

- **Request/Response Models**: Standardized data transfer objects for client-service communication
- **Type Safety**: Strongly-typed models that leverage LogicBuilder's domain and expression infrastructure
- **Serialization Support**: JSON-serializable structures with custom converters for complex types
- **Error Handling**: Built-in error messaging and success indicators in response objects

## Key Features

### Request Types

All request types implement `IBaseRequest` and support various data operations:

- **`SaveEntityRequest`**: Save or update domain entities (`BaseModel` instances)
- **`DeleteEntityRequest`**: Delete domain entities
- **`GetEntityRequest`**: Retrieve a single entity using filters and select/expand definitions
- **`GetObjectListRequest`**: Query and retrieve lists of objects with projection capabilities
- **`GetTypedListRequest`**: Retrieve strongly-typed lists with specific return types

### Response Types

All response types inherit from `BaseResponse` and include:

- **`Success`**: Boolean flag indicating operation outcome
- **`ErrorMessages`**: Collection of error messages if the operation failed
- **`TypeString`**: Assembly-qualified type name for polymorphic deserialization

Specific response types:

- **`SaveEntityResponse`**: Returns the saved entity
- **`DeleteEntityResponse`**: Confirms deletion status
- **`GetEntityResponse`**: Returns the requested entity
- **`GetListResponse`**: Returns a collection of `BaseModel` entities
- **`GetObjectListResponse`**: Returns a collection of generic objects
- **`ErrorResponse`**: General-purpose error response

### Advanced Query Capabilities

Requests support LogicBuilder's expression descriptors for dynamic query building:

- **`FilterLambdaDescriptor`**: Define filtering conditions
- **`SelectorLambdaDescriptor`**: Specify projection and transformation logic
- **`SelectExpandDefinitionDescriptor`**: Control which properties to include/expand

## Installation

Install via NuGet Package Manager:

```bash
dotnet add package LogicBuilder.App.Bsl.Business
```

Or via Package Manager Console:

```powershell
Install-Package LogicBuilder.App.Bsl.Business
```

## Usage

### Basic CRUD Operations

**Save Entity Example:**

```csharp
using LogicBuilder.App.Bsl.Business.Requests;
using LogicBuilder.App.Bsl.Business.Responses;
using LogicBuilder.Domain;

// Create a save request
var saveRequest = new SaveEntityRequest
{
    Entity = new MyEntity { Id = 1, Name = "Example" }
};

// Send to business service and receive response
SaveEntityResponse response = await businessService.SaveAsync(saveRequest);

if (response.Success)
{
    Console.WriteLine($"Saved entity: {response.Entity}");
}
else
{
    Console.WriteLine($"Errors: {string.Join(", ", response.ErrorMessages)}");
}
```

**Get Entity Example:**

```csharp
using LogicBuilder.Expressions.Utils.ExpressionDescriptors;
using LogicBuilder.Expressions.Utils.ExpansionDescriptors;

var getRequest = new GetEntityRequest
{
    Filter = new FilterLambdaDescriptor(
        new EqualsBinaryDescriptor(
            new MemberSelectorDescriptor("Id", new ParameterDescriptor("entity")),
            new ConstantDescriptor(1)
        ),
        typeof(MyEntity).AssemblyQualifiedName,
        "entity"
    ),
    SelectExpandDefinition = new SelectExpandDefinitionDescriptor(
        new[] { "Id", "Name" },
        new[] { new SelectExpandItemDescriptor("RelatedData") }
    ),
    ModelType = typeof(MyEntity).AssemblyQualifiedName,
    DataType = typeof(MyEntityData).AssemblyQualifiedName
};

GetEntityResponse response = await businessService.GetAsync(getRequest);
```

### Serialization

All requests and responses are JSON-serializable:

```csharp
using System.Text.Json;

// Configure JSON options
var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};
options.Converters.Add(new LogicBuilder.Domain.Json.ModelConverter());

// Serialize request
string json = JsonSerializer.Serialize(saveRequest, options);

// Deserialize response
var response = JsonSerializer.Deserialize<SaveEntityResponse>(json, options);
```

### Error Handling

All responses include built-in error handling:

```csharp
BaseResponse response = await businessService.ExecuteAsync(request);

if (!response.Success)
{
    foreach (var error in response.ErrorMessages)
    {
        Console.WriteLine($"Error: {error}");
    }
}
```

## Dependencies

This library depends on:

- **LogicBuilder.App.Utils** - Utility functions and helpers
- **LogicBuilder.Domain** - Base domain models (`BaseModel`) and JSON converters
- **LogicBuilder.Structures** - Expression descriptors for dynamic query building

## Target Framework

- **.NET Standard 2.0** - Compatible with .NET Framework 4.6.1+ and .NET Core 2.0+

## Project Structure

```
LogicBuilder.App.Bsl.Business/
├── Requests/
│   ├── IBaseRequest.cs              # Base interface for all requests
│   ├── SaveEntityRequest.cs         # Save/update entity operations
│   ├── DeleteEntityRequest.cs       # Delete entity operations
│   ├── GetEntityRequest.cs          # Retrieve single entity
│   ├── GetObjectListRequest.cs      # Query object collections
│   └── GetTypedListRequest.cs       # Query typed collections
└── Responses/
    ├── BaseResponse.cs              # Abstract base with common properties
    ├── SaveEntityResponse.cs        # Returns saved entity
    ├── DeleteEntityResponse.cs      # Confirms deletion
    ├── GetEntityResponse.cs         # Returns retrieved entity
    ├── GetListResponse.cs           # Returns entity collections
    ├── GetObjectListResponse.cs     # Returns object collections
    ├── ErrorResponse.cs             # General error response
    └── Json/
        └── ResponseConverter.cs     # Custom JSON converter for polymorphic responses
```

## Testing

The library includes comprehensive unit tests (targeting .NET 10) covering:

- Request/response serialization and deserialization
- Null value handling
- Complex query descriptor structures
- Error scenarios and validation

## Contributing

Contributions are welcome! Please refer to the main [LogicBuilder repository](https://github.com/BpsLogicBuilder/LogicBuilder) for contribution guidelines.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Links

- [GitHub Repository](https://github.com/BpsLogicBuilder/LogicBuilder.App.Bsl.Business)
- [NuGet Package](https://www.nuget.org/packages/LogicBuilder.App.Bsl.Business/)
- [LogicBuilder Project](https://github.com/BpsLogicBuilder/LogicBuilder)

## Support

For issues, questions, or contributions, please visit the [GitHub Issues](https://github.com/BpsLogicBuilder/LogicBuilder.App.Bsl.Business/issues) page.