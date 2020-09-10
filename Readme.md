# Installation

## 1. Requirements

### 1.1. Sql Part
1. MsSql server
2. Create database, and don't forget to change database name on configuration(appsettings.json) on RecipeAPI
2. Make sure that you have set your Startup Project as RecipeAPI and the Default Project as DataAccess.EFCore for step 3
3. You need to update database with 'update-database' command. You can write that command with Package Manager Console on Visual Studio
4. (Optional) If you want to change Entities, make sure run 'add-migration migration_name' command. After that, repeat 2nd, 3th steps.

### 1.2. NoSql Part
1. MongoDb server

## 2. Configuration
1. You can set up your database settings from appsettings.json on RecipeAPI
2. You can change 'UseNoSql' parameter for switch around EFCore and MongoDb.Driver from appsettings.json on RecipeAPI