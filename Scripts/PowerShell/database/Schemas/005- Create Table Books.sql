USE [data_analysis_book];
Go

BEGIN TRY
    BEGIN TRANSACTION;

    IF EXISTS (SELECT name FROM sys.tables WHERE name = N'Books')
    BEGIN
        DROP TABLE [Books];
    END

    CREATE TABLE Books
    (
        Id INT PRIMARY KEY IDENTITY(1, 1),
        Author INT NOT NULL,
        Code NVARCHAR(21) NULL,
        Title NVARCHAR(365) NOT NULL,
        Description NVARCHAR(max) NOT NULL,
        Publisher INT NOT NULL,
        Edition NVARCHAR(221) NOT NULL,
        Stock INT NOT NULL,
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