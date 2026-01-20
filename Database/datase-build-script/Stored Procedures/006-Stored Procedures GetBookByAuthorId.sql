USE [data_analysis_book];
GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'GetBookByAuthorId') AND type = N'P')
BEGIN
    DROP PROCEDURE [GetBookByAuthorId];
END
GO

CREATE PROCEDURE GetBookByAuthorId
    @id INT = null
AS
BEGIN
	select
		Id,
		Author,
		Code,
		Title,
		Description,
		Publisher,
		Edition,
		Stock,
		CreationDate,
		ModificationDate
	from Books
	where Author = @id

END
Go