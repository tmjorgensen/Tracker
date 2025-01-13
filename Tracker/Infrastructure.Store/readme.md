# Migrations

## Setup for multiple database providers

To support multiple database providers, a separate migrations project has been created for each. Refer to [this](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/projects?tabs=vs) for information, but a quick break-down of the steps is:

1. Create a project with the DbContext.

2. Generate an initial (empty) migration for that project.

3. Create a new project for each database provider.
    1. Add a reference pointing to the project with the DbContext.
    2. Copy the initial migration and snapshot to this project.
    3. Add a reference from the startup project to this project.

4. Once all database providers have been setup, delete the initial migration and snapshot from the project with the DbContext.

## Dependency Injection

To setup the dependency injection for the DbContext in the startup project with multiple database providers, refer to [this]8https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/projects?tabs=vs).

Basically you need to configure which provider you are going to use, then give it the correct connection string and assembly with migration files.

## Add migrations

To add migrations you need to run the following command once per database provider:

```
Add-Migration -Name <name> -Args "--<provider config key> <provider config value>"
```

Note that the argument `<provider config key>` is the key in your configuration that determines which provider to use. Likewise, `<provider config value>` is the value you have chosen for a specific databsae provider.

You may have to set the `Default Project` in the Package Manager Console to the desired migrations projcet too.
