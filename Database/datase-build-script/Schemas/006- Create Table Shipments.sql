USE [data_analysis_book];
Go

BEGIN TRY
    BEGIN TRANSACTION;

    IF EXISTS (SELECT name FROM sys.tables WHERE name = N'Shipments')
    BEGIN
        DROP TABLE [Shipments];
    END

    CREATE TABLE Shipments(
        Id INT PRIMARY KEY IDENTITY(1, 1),
        OriginLocations VARCHAR(max) NOT NULL,
        DestinationLocations VARCHAR(max) NOT NULL,
        Notes VARCHAR(max) NOT NULL,
        Budget DECIMAL(12,6) NOT NULL,
        Code VARCHAR(50) NOT NULL,
        Stock INT NOT NULL,
        DeliveryDate DATETIME NULL,
        ReservationDate DATETIME NULL,
        Confirmation BIT NULL,
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