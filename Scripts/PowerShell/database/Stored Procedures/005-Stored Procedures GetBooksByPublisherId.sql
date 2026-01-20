USE [data_analysis_book];
GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'GetBooksByPublisherId') AND type = N'P')
BEGIN
    DROP PROCEDURE [GetBooksByPublisherId];
END
GO

CREATE PROCEDURE GetBooksByPublisherId
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
	where Publisher = @id

END
Go