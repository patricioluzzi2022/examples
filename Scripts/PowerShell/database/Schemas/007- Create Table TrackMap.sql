USE [data_analysis_book];
Go

BEGIN TRY
    BEGIN TRANSACTION;

    IF EXISTS (SELECT name FROM sys.tables WHERE name = N'TrackMap')
    BEGIN
        DROP TABLE [TrackMap];
    END

    CREATE TABLE TrackMap(
        Id INT PRIMARY KEY IDENTITY(1, 1),
        TrackingCode VARCHAR(50) NOT NULL,
        ShipmentId INT NOT NULL,
        ReceptionDate DATETIME NOT NULL,
        Notes VARCHAR(max) NOT NULL,
        DocumentPath VARCHAR(max) NOT NULL, -- image of the document path
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