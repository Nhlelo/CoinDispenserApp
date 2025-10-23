[![CI — build & test](https://github.com/Nhlelo/CoinDispenserApp/actions/workflows/cicdpipeline.yml/badge.svg)](https://github.com/Nhlelo/CoinDispenserApp/actions/workflows/cicdpipeline.yml)

Coin Dispenser Solution
A simple end-to-end .NET solution that computes minimum coin change.

Projects:
- CoinDispenser.Core — algorithm, DTOs, interfaces (class library).
- CoinDispenser.Api — ASP.NET Core Web API, EF Core persistence, middleware (web project).
- CoinDispenser.Web — Razor Pages frontend that consumes the API (web project).
- CoinDispenser.Tests — xUnit unit tests (tests project).
