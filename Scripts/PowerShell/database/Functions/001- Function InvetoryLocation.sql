USE [data_analysis_book];
Go

--------------------------------------------------
-- Function GetInventoryLocation -----------------
-- Description: Get the InvenroryLocation --------
-- information fields filtering by the code and --
-- the nickname ----------------------------------
CREATE FUNCTION GetInventoryLocation
(
    @book INT,
    @nickname NVARCHAR(365) = null
)
RETURNS NVARCHAR(MAX)
AS
BEGIN

    DECLARE @InventoryLocation NVARCHAR(MAX);

	if @nickname is not null
    BEGIN
        select
            @InventoryLocation = CONCAT(Nickname, ' ', Page,' ' , TheMessage, ' ',RowNumber, ' ', ColumnNumber)
        from InventoryLocation il
        left join Notes n on n.Book = il.Book
        where il.Book = @book
        and n.Nickname = @nickname;

    END
    ELSE
    BEGIN
        select
            @InventoryLocation = CONCAT(shelf, ' ', RowNumber, ColumnNumber)
        from InventoryLocation
        where Book = @book;

    END

	RETURN @InventoryLocation

END
GO