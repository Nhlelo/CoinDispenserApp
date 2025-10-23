[![CI — build & test](https://github.com/Nhlelo/CoinDispenserApp/actions/workflows/cicdpipeline.yml/badge.svg)](https://github.com/Nhlelo/CoinDispenserApp/actions/workflows/cicdpipeline.yml)

Coin Dispenser Solution
A simple end-to-end .NET solution that computes minimum coin change.

Projects:
- CoinDispenser.Core — algorithm, DTOs, interfaces (class library).
- CoinDispenser.Api — ASP.NET Core Web API, EF Core persistence, middleware (web project).
- CoinDispenser.Web — Razor Pages frontend that consumes the API (web project).
- CoinDispenser.Tests — xUnit unit tests (tests project).


Prerequisites
- .NET 9 SDK (or adjust target frameworks to your installed SDK)
- dotnet-ef tool if you want to create migrations locally: `dotnet tool install --global dotnet-ef`
- SQL (must have a local instance)

Build
- From solution root:
  - `dotnet restore`
  - `dotnet build`

API - run
- cd CoinDispenser.Api
- Ensure appsettings.json contains desired ApiKey and connection string
- The program auto-applies migrations on startup.
- `dotnet run`
- Default URL: https://localhost:44373 (Kestrel prints actual URL)
- API endpoint: POST https://localhost:44373/api/coinchange
  - Body JSON: `{ "denominations":[1,2,5,10], "amount":18 }`

Apply migrations manually (optional)
- cd CoinDispenser.Api
- `dotnet ef migrations add InitialCreate`
- `dotnet ef database update`

Web frontend - run
- cd CoinDispenser.Web
- Ensure appsettings.json contains `ApiBaseUrl` matching the API
- `dotnet run`
- Open browser at the URL printed (e.g., https://localhost:44358)

Tests
- cd tests/CoinDispenser.Tests
- `dotnet test`

Configuration
- appsettings.json present in Api and Web projects


Database
- SQL

Screenshots of Application 
<img width="950" height="509" alt="image" src="https://github.com/user-attachments/assets/a7b7f84f-e7d0-4d03-9159-9937b57883fb" />

<img width="956" height="504" alt="image" src="https://github.com/user-attachments/assets/dad0738a-d30f-4f60-a459-0dff6f9db5d7" />


