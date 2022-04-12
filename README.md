# ApiGrid

This project corresponds to the Api Backend. Project was created within the Visual Studio Comunnite 2022 environment along with the SQL Server Database.
For the execution of the project I used the Proprio Visual Studio environment and the API is documented with Swagger.
The Entity Framework and AspNet packages were used primarily.

# The Database:
The settings are in appsettings.json. I used windows security settings.
He named the database GridData and used a set of migrations to start the database and tables.

# The project:
It is basically composed of two controllers, their respective models and services. In addition to a pagination and filtering configuration.
For the Files controller, the methods were implemented
post files
Delete Files
Get Files.
For the Samples controller, the following methods were implemented:
Get Samples by File
Get Min Sampales by File
Get Max Samples by File
Get Range most expensive by File

# Execution:
For the execution I used IIS Express directly, because in the frontend I statically mapped the route and the API port.
In the file lauchSettings.json you can find the route settings.