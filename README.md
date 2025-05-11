# FandomHub API

This is the API project for FandomHub, built using ASP.NET Core.

## Configuration

To run the project, you need to create a configuration file named `appsettings.json` inside the `FandomHub.API` project folder.

### Steps:

1. Create `appsettings.json` in project `FandomHub.API` directory.
2. Copy the provided `appsettings.example.json` file and paste to `appsettings.json`.
3. Fill in the required values such as connection strings, JWT secrets, API keys, etc.

> ⚠️ Do not commit `appsettings.json` to the repository. It is ignored via `.gitignore`.

## Setting Up the Database

After configuring `appsettings.json`, run the following commands to apply migrations and update the database.

### Using Package Manager Console

1. Open **Package Manager Console** in Visual Studio.
2. Set the **Default Project** to `FandomHub.Infrastructure`.
3. Run the following commands:

```powershell
1. Add-Migration InitialCreate
2. Update-Database