# dotnet-langsearch-sdk 🌐🔍

[![NuGet](https://img.shields.io/nuget/v/Estrada.LangSearch.SDK.svg)](https://www.nuget.org/packages/Estrada.LangSearch.SDK)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-10.0%2B-blue.svg)](https://dotnet.microsoft.com/)

A powerful and easy-to-use SDK for C# developers to integrate with LangSearch APIs, enabling seamless web search and semantic reranking capabilities in your .NET applications.

## ✨ What's in the Box

This SDK provides everything you need to get started with LangSearch:

- **ILangSearchClient**: A typed HttpClient wrapper that leverages .NET's built-in HttpClient for making requests to LangSearch Web Search and Rerank APIs. Ready for dependency injection!
- **ILangSearchService**: A high-level application service that provides LangSearch data in a more user-friendly format. Perfect for injecting into your services.
- **LangSearchSettings**: A configuration class to manage LangSearch settings (including API key) from your `appsettings.json`, injectable as `IOptions<LangSearchSettings>`.
- **POCO Classes**: Strongly-typed C# classes for all LangSearch models, ensuring type safety and ease of use.
- **Startup Extensions**: Convenient extension methods for integrating the SDK into your .NET projects with minimal setup.

## 🏗️ How It Works

The SDK follows a layered architecture designed for flexibility and ease of use:

```
ILangSearchService (High-level service)
          |
          v
ILangSearchClient (Typed HttpClient)
          |
          v
LangSearch API (Third-party service)
```

- **ILangSearchService**: Communicates with the client, applies transformations, and returns data in a convenient format.
- **ILangSearchClient**: Handles HTTP requests to the LangSearch API and manages communication exceptions.
- **LangSearch API**: The external service providing web search and reranking functionality.

You can inject `ILangSearchService` for a full-featured experience or `ILangSearchClient` for direct API access.

## 🚀 Quick Start

### 1. Configuration

Add the following section to your `appsettings.json`:

```json
{
  "LangSearchSettings": {
    "Domain": "https://api.langsearch.com",
    "WebSearchEndpoint": "v1/web-search",
    "RankedSearchEndpoint": "v1/rerank",
    "ApiKey": "<your-api-key-here>"
  }
}
```

> **Note**: The configuration section **must** be named `LangSearchSettings` for the SDK to recognize it.

### 2. Setup in Startup/Program.cs

Choose the integration level that fits your needs:

#### Full Setup (Recommended)
```csharp
builder.Services.ConfigureLangSearch(builder.Configuration);
```

#### Modular Setup
```csharp
// Configure settings
builder.Services.AddLangSearchConfiguration(builder.Configuration);

// Add typed HttpClient
builder.Services.AddLangSearchClient(builder.Configuration);

// Add service with desired lifetime
builder.Services.AddLangSearchService(builder.Configuration, ServiceLifetime.Scoped);
```

### 3. Usage

Inject and use the service in your classes:

```csharp
public class MyService
{
    private readonly ILangSearchService _langSearchService;

    public MyService(ILangSearchService langSearchService)
    {
        _langSearchService = langSearchService;
    }

    public async Task SearchWebAsync(WebSearchRequest request)
    {
        var results = await _langSearchService.GetWebSearchData(WebSearchRequest);
        // Process results...
    }
}
```

## 📋 API Overview

| API | Description | Use Case |
|-----|-------------|----------|
| **Web Search** | Gathers text data from the web with optional filtering by data source lifespan | Retrieve current or historical web content |
| **Semantic Rerank** | Performs similarity search on documents to score relevance to user queries | Improve search result ranking and relevance |

## 🛠️ Sample Implementation

This repository includes a sample project: [**dotnet-llamasharp**](https://github.com/AEstradaGrech/dotnet-llamasharp)

It demonstrates how to use the SDK to gather data and feed a RAG (Retrieval-Augmented Generation) service, showcasing real-world integration.

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

Made with ❤️ for the .NET community. Get it for free in the Package Manager! 📦 
