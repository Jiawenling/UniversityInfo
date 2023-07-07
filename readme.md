# ASP.NET Core Web API

A simple REST API demonstrating the use of JWT token, Sqlite

## Prerequisites 

.NET Core Version
This project is built using .NET Core 7. To run this project, you will 
need to have .NET Core 7 installed on your machine. You can download the 
latest version of .NET Core from the official Microsoft website.

System Requirements
This project will run on any OS.

## Endpoints:

Certain endpoints requires an authorised user, please refer to next section for more information

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

The program will seed data using this [json file](UniversityInfo/SeedData/Universities.json) on startup - this can be disabled under 
Program.cs:
```csharp
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DataContext>();
    context.Database.Migrate();
    DataInitialiser.SeedData(context).Wait(); //optional
}
```
You can run the program by using the following command
```
dotnet run
```

Once the program is running, you can import this [json file](UniversityInfo/UniversityInfo.postman_collection.json) into PostMan to start testing the endpoints

### Accessing secured endpoints

Certain endpoints are only accessible by authorised users. Follow the steps below to get a JWT token:

* POST `/authentication`

    * Register the user and adds it to the database
    * Request Body Example:

        ```json
        {
            "Username": "adityaoberai1",
            "Password": "test123"
        }
        ```

* POST `/authentication/login`

    * Returns the JWT token 
    * Request Body Example:

        ```json
        {
            "Username": "adityaoberai1",
            "Password": "test123"
        }
        ```

    * Response Example:

        ```json
        { "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiYWRpdHlhb2JlcmFpMSIsImdpdmVuX25hbWUiOiJBZGl0eWEiLCJyb2xlIjoiRXZlcnlvbmUiLCJuYmYiOjE2NjA3NzA0NDQsImV4cCI6MTY2MDc3MjI0NCwiaWF0IjoxNjYwNzcwNDQ0fQ.20KEe53MsDeapYk0EkeayfZqmsyPSuVOVBzsHpmFMS4",
        }
        ```

  * To access a secured endpoint you must add the following header into your payload.
    * Example:

        `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiYWRpdHlhb2JlcmFpMSIsImdpdmVuX25hbWUiOiJBZGl0eWEiLCJyb2xlIjoiRXZlcnlvbmUiLCJuYmYiOjE2NjA3NzA0NDQsImV4cCI6MTY2MDc3MjI0NCwiaWF0IjoxNjYwNzcwNDQ0fQ.20KEe53MsDeapYk0EkeayfZqmsyPSuVOVBzsHpmFMS4`

### Filtering and pagination for GET/University
Filtering and pagination are acheived by using ASP .Net Core OData 8 library

* Pagination: https://learn.microsoft.com/en-us/odata/webapi-8/fundamentals/client-driven-paging 
Example:

```
http://localhost:8443/University?$skip=3&$top=1
```
* Filtering: https://learn.microsoft.com/en-us/aspnet/web-api/overview/odata-support-in-aspnet-web-api/supporting-odata-query-options
Example:

```
http://localhost:8443/University?$filter=Country eq 'USA'
```
