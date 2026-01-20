USE [data_analysis_book];
Go

BEGIN TRY
    BEGIN TRANSACTION;

    IF EXISTS (SELECT name FROM sys.tables WHERE name = N'TemporaryTable')
    BEGIN
        DROP TABLE [TemporaryTable];
    END

    CREATE TABLE TemporaryTable(
        Id INT PRIMARY KEY IDENTITY(1, 1),
        IndexNumber INT NOT NULL,
        PageIndex INT NOT NULL,
        Page INT NULL,
        PageText VARCHAR(max) NULL,
        DocumentPath VARCHAR(max) NOT NULL,
        Category VARCHAR(50) NOT NULL,
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