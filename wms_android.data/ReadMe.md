# MIGRATION NOTICE: Deprecated Project

This project (`wms_android.data`) is being phased out in favor of the shared codebase (`wms_android.shared`).

## Why We're Migrating

The `wms_android.data` project contained database context definitions that have been moved to the shared project to allow for better code reuse across the API and Android applications. Having the data structures defined in only one location makes maintenance easier and eliminates duplication.

## What's Different

The main difference is the namespace. The `AppDbContext` class has been moved from `wms_android.data.Data` to `wms_android.shared.Data`.

## Migration Steps

1. Update your using directives:
   ```csharp
   // OLD
   using wms_android.data.Data;
   
   // NEW
   using wms_android.shared.Data;
   ```

2. If you have any DI container registrations:
   ```csharp
   // OLD
   services.AddDbContext<wms_android.data.Data.AppDbContext>(options => 
       options.UseNpgsql(connectionString));
   
   // NEW
   services.AddDbContext<wms_android.shared.Data.AppDbContext>(options => 
       options.UseNpgsql(connectionString));
   ```

3. If you're using the `IDesignTimeDbContextFactory` for migrations, make sure it's pointing to the new shared context.

## Migrations

The migrations in this project are still valid and will continue to work with the shared context. However, all future migrations should be created in the `wms_android.api` project against the shared context.

## Timeline

This project will be maintained for backward compatibility until all consumers have migrated to the shared codebase. After that, this project will be removed.

## Questions?

If you have any questions about the migration process, please contact the architecture team. 