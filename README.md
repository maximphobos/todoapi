# ДЗ 7. TODO API
This is a ASP.Net Core API application for managing TODO list of tasks.
You should have installed MsSQL server on your machine to use this app (or, if you want to use another database's server, please intall it's EF related nuget package and apply changes in ToDoListContext on 24 line).

To run the application please, do the required steps below:
1. Build app
2. Set your connection string details in appsettings.json for the DefaultConnection (change existing)
3. In Visual Studio Package Manager Console run:
   Update-Database -Context ApplicationDbContext 
   and 
   Update-Database -Context ToDoListContext  
4. Run app and test it in Swagger UI
