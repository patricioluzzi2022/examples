USE [data_analysis_book];
GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'GetPublishersByCode') AND type = N'P')
BEGIN
    DROP PROCEDURE [GetPublishersByCode];
END
GO

CREATE PROCEDURE GetPublishersByCode
    @code nvarchar(50),
	@position int = null
AS
BEGIN
	select
		Id,
		Code,
		Name,
		CreationDate,
		ModificationDate
	from Publishers
	where dbo.GetPublisherCode(Code, @position) = dbo.GetPublisherCode(@code, @position)

END
Go