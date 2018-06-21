# Assignment

## Solution structure

 - `src/Catalog.Api` - catalog service
 - `src/Catalog.Web` - SPA application
 - `tests/*` - unit tests projects
 - `run.cmd` - start application

## Requires

 - [.NET Core 2.1 SDK](https://www.microsoft.com/net/download/windows)
 - [Node.js 8.x.x](https://nodejs.org/en/download/)

## Run application

To start the application execute `run.cmd`. Open [http://localhost:5000](http://localhost:5000) in browser.

## Database connection string

_Catalog.Api_ service uses SQL database to store data. Sample database deployed in Azure. 
```
Server=tcp:suitsupply.database.windows.net,1433;Initial Catalog=catalog;Persist Security Info=False;User ID=andrey;Password=hello-catalog-1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```