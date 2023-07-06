# ASP.NET Core Web API

A simple REST API using JWT tokens, Sqlite

## Prerequisites 

.NET Core Version
This project is built using .NET Core 7. To run this project, you will 
need to have .NET Core 7 installed on your machine. You can download the 
latest version of .NET Core from the official Microsoft website.

System Requirements
This project will run on any OS.


In order to test the console application run the following commands from 
the command line:

```
dotnet build
```

You can run the application using the .dll found in /bin

```
dotnet University.dll
```

## How To Run

Configure JWT secret and name of database under appsettings.json
```
{
  "JwtSettings": {
    "Issuer": "localhost:8443",
    "Audience": "localhost:8443",
    "Key": "<SECRETKEY>",
    "Expiration": "30"
},
"ConnectionStrings": {
  "sqlite": "Data Source=UniversityDatabase.db"
},
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

The program will seed data on startup - this can be disabled under 
Program.cs:
```
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DataContext>();
    context.Database.Migrate();
    DataInitialiser.SeedData(context).Wait(); //comment out this line if 
needed
}
```
##The following endpoints are supported:

### GET
GET/university: Retrieve a list of universities. <br/>
GET/university/{id}: Retrieve details of a specific university by its ID.
### POST
POST/Authentication: Register as user<br/>
POST/Authentication/Login: Login as user<br/>
POST/university/bookmark/{id}: Bookmark a specific university <br/>
`Authorised users only` POST/university: Create a new university <br/>
### PUT
`Authorised users only` PUT/university/{id}: Update details of a specific 
university <br/>
### DELETE
`Authorised users only` DELETE/university/{id}: Delete a specific 
university <br/>


___
