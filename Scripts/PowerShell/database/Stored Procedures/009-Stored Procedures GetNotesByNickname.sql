USE [data_analysis_book];
GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'GetNotesByNickname') AND type = N'P')
BEGIN
    DROP PROCEDURE [GetNotesByNickname];
END
GO

CREATE PROCEDURE GetNotesByNickname
    @bookId int = null,
	@nickname nvarchar(max)
AS
BEGIN
	select
		id,
		Book,
		TheMessage,
		Nickname,
		Page,
		CreationDate,
		ModificationDate
	from Notes
	where Book = isnull(@bookId, Book)
	and Nickname like '%'+@nickname+'%'

END
Go