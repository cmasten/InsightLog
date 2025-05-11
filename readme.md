# InsightLog 🧠📓

![CI](https://github.com/cmasten/InsightLog/actions/workflows/ci.yml/badge.svg)

**InsightLog** is a journaling and productivity system built to showcase development practices using the modern .NET ecosystem, including clean architecture, CQRS, development workflows, and DevOps tooling.

---

## Features

- Create and manage **journal entries**, with optional AI-generated summaries
- Define and track **goals** with deadlines and progress history
- Schedule and complete **recurring habits**
- Dashboard summarizing trends, AI insights, and completion stats
- Organized in a clean, testable, and extensible format

---

## Technology Stack

| Area              | Tools / Libraries |
|-------------------|-------------------|
| API               | .NET 9, ASP.NET Core, MediatR |
| Domain            | DDD-inspired Aggregates, Value Objects, Strongly Typed IDs |
| Data              | EF Core, SQLite (PostgreSQL planned), Migrations |
| Architecture      | CQRS, Clean Architecture, Vertical Slices |
| API Tooling       | Swagger, API Versioning |
| DevOps            | GitHub Actions, Docker, Docker Compose |
| Observability     | Serilog (Console + File), Structured Logging |
| Testing           | xUnit, FluentAssertions, In-Memory EF Tests |
| CI/CD             | GitHub Actions (Build + Test on Push/PR) |

---

## 🧪 Running the Project

### Requirements
- [.NET 9 SDK](https://dotnet.microsoft.com/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Run with Docker

```bash
docker compose up --build
