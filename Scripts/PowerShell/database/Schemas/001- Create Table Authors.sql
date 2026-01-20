USE [data_analysis_book];
Go

BEGIN TRY
    BEGIN TRANSACTION;

    IF EXISTS (SELECT name FROM sys.tables WHERE name = N'Authors')
    BEGIN
        DROP TABLE [Authors];
    END

    CREATE TABLE Authors
    (
        Id INT PRIMARY KEY IDENTITY(1, 1),
        FirstName NVARCHAR(100) NOT NULL,
        LastName NVARCHAR(300) NOT NULL,
        Nationality NVARCHAR(100) NOT NULL,
        BirthDate DATE NOT NULL,
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