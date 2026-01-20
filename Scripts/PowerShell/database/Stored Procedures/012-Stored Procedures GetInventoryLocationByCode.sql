USE [data_analysis_book];
GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'GetInventoryLocationByCode') AND type = N'P')
BEGIN
    DROP PROCEDURE [GetInventoryLocationByCode];
END
GO

CREATE PROCEDURE GetInventoryLocationByCode
    @code nvarchar(50),
	@position int = null
AS
BEGIN
	select
		Id,
		Book,
		Shelf,
		Code,
		RowNumber,
		ColumnNumber,
		Branch,
		CreationDate,
		ModificationDate
	from InventoryLocation
	where dbo.GetInventoryLocationCode(Code, @position) = dbo.GetInventoryLocationCode(@code, @position)

END
Go