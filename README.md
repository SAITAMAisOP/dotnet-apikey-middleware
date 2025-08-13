# ASP.NET Core Web API – API Key Authentication Middleware

This project demonstrates how to secure ASP.NET Core Web API endpoints using a **custom API Key validation middleware**.

## Features

- Validates API requests using an API key.
- API key can be provided via:
    - **HTTP Header**: `X-ApiKey`
    - **Query String**: `?X-ApiKey=your_key`
- Returns `401 Unauthorized` if:
    - API key is missing.
    - API key is invalid.
- Throws an error if the API key is not configured in the application settings.

---

## Middleware Overview

The `ApiKeyValidationMiddleware`:
1. Reads the configured API key from `IConfiguration` (`Authentication:ApiKey`).
2. Checks for the API key in:
    - `X-ApiKey` request header.
    - Query string `X-ApiKey`.
3. Validates the provided API key against the configured one.
4. Allows the request through if valid, otherwise responds with `401 Unauthorized`.

```csharp
app.UseMiddleware<ApiKeyValidationMiddleware>();
