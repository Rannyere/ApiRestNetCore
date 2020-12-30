# ApiRestNetCore

Commands Generate Database with EntityFramework
/* ControlDbContext */ 
dotnet ef migrations add Initial_DB --project IO.Data -s IO.App --context ControlDbContext --verbose 
dotnet ef database update Initial_DB --project IO.Data -s IO.ApiRest --context ControlDbContext --verbose

/* ApplicationDbContext */ 
dotnet ef migrations add Identity --project IO.ApiRest -s IO.ApiRest --context ApplicationDbContext --verbose 
dotnet ef database update Identity --project IO.ApiRest -s IO.ApiRest --context ApplicationDbContext --verbose

Nuget Packages

//Identity
Microsoft.AspNetCore.Identity.EntityFrameworkCore
Microsoft.AspNetCore.Identity.UI

//EntityFramework
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Design

//MySQL
Pomelo.EntityFrameworfCore.MySql

//AutoMapper
AutoMapper.Extensions.Microsoft.DependencyInjection

//JWT
Microsoft.AspNetCore.Authentication.JwtBearer

//Versioning and Swagger
Microsoft.AspNetCore.Mvc.Versioning
Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
Swashbuckle.AspNetCore

// Logger
elmah.io.aspnetcore

//HealthChecks
AspNetCore.HealthChecks.MySql
AspNetCore.HealthChecks.UI
AspNetCore.HealthChecks.UI.Client