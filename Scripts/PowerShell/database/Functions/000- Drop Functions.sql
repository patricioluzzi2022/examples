USE [data_analysis_book];
Go

BEGIN TRY
    BEGIN TRANSACTION;

    IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'GetInventoryLocation') AND type = N'FN')
    BEGIN
        DROP FUNCTION [GetInventoryLocation];
    END

    IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'GetCodeByPosition') AND type = N'FN')
    BEGIN
        DROP FUNCTION [GetCodeByPosition];
    END

    IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'GetNoteId') AND type = N'FN')
    BEGIN
        DROP FUNCTION [GetNoteId];
    END

    IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'GetPublisherCode') AND type = N'FN')
    BEGIN
        DROP FUNCTION [GetPublisherCode];
    END

    IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'GetInventoryLocationCode') AND type = N'FN')
    BEGIN
        DROP FUNCTION [GetInventoryLocationCode];
    END

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