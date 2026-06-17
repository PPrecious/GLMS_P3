# GLMS_P3

ASP.NET Core distributed enterprise logistics system for managing clients, contracts and service requests. 
The solution has been refactored from a monolithic MVC architecture to a Service-Oriented Architecture (SOA) with dedicated ASP.NET Core Web API as core backend service. 
Secure JWT authentication is provided with centralized business logic in the API layer. Decoupled presentation is enabled by an MVC frontend that consumes API endpoints using HttpClient.

The architecture is based on Clean Architecture principles with separation of concerns in API, MVC client, and data layers making the application scalable and maintainable.
The system is fully containerized, utilizing Docker Compose, with SQL Server, the GLMS Web API and the MVC frontend operating as separate services communicating over internal networking.

Extensive automated unit and integration testing is performed using xUnit to ensure the reliability of critical business logic including currency calculations, authentication flow and API endpoint validation.

It is designed for cloud ready deployment, horizontal scalability and enterprise level extensibility and adheres to modern DevOps and CI/CD practices.
