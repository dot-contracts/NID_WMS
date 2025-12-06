-- Set NULL for any CreatedById values that don't reference valid User IDs
UPDATE "Parcels" 
SET "CreatedById" = NULL 
WHERE "CreatedById" IS NOT NULL 
  AND NOT EXISTS (
    SELECT 1 FROM "Users" WHERE "Users"."Id" = "Parcels"."CreatedById"
  ); 