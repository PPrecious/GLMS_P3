# GLMS_P3

ASP.NET Core distributed enterprise logistics system for managing clients, contracts, and service requests. 
The solution has been refactored from a monolithic MVC architecture into a Service-Oriented Architecture (SOA) using a dedicated ASP.NET Core Web API as the core backend service. 
The system supports secure JWT authentication, centralized business logic in the API layer, and decoupled presentation via an MVC frontend consuming API endpoints through HttpClient.

It includes file uploads (PDF-only) with server-side validation and secure download handling, real-time currency conversion (USD to ZAR via external API integration), 
and automated business rule enforcement such as contract status workflows and service request validation.

The architecture follows Clean Architecture principles with separation of concerns across API, MVC client, and data layers, enabling scalability and maintainability.
The system is fully containerized using Docker Compose, orchestrating SQL Server, the GLMS Web API, and the MVC frontend as independent services communicating through internal networking.

Comprehensive automated unit and integration testing using xUnit ensures reliability of critical business logic, including currency calculations, authentication flow, and API endpoint validation.

The solution is designed for cloud-ready deployment, horizontal scalability, and enterprise-level extensibility, aligning with modern DevOps and CI/CD practices.
