USE [data_analysis_book];
Go

BEGIN TRY
    BEGIN TRANSACTION;

    IF EXISTS (SELECT name FROM sys.tables WHERE name = N'InventoryLocation')
    BEGIN
        DROP TABLE [InventoryLocation];
    END

    CREATE TABLE InventoryLocation
    (
        Id INT PRIMARY KEY IDENTITY(1, 1),
        Book INT NOT NULL,
        Shelf VARCHAR(MAX) NOT NULL,
        Code NVARCHAR(50) NOT NULL,
        RowNumber decimal(12,6) NOT NULL,
        ColumnNumber decimal(12,6) NOT NULL,
        Branch VARCHAR(max) NOT NULL,
        CreationDate DATETIME NULL,
        ModificationDate DATETIME NULL
    );

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
        -- Rollback the transaction if an error occurs
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Print the error message
        Print 'Lokilog occurred: ' + ERROR_MESSAGE(); --) Book
        Print 'Lokilog number: ' + CAST(ERROR_NUMBER() AS NVARCHAR(10)); --) Page
        Print 'Lokilog state: ' + CAST(ERROR_STATE() AS NVARCHAR(10)); --) Author

END CATCH