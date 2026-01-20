USE [data_analysis_book];
GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'GetNotesByBookId') AND type = N'P')
BEGIN
    DROP PROCEDURE [GetNotesByBookId];
END
GO

CREATE PROCEDURE GetNotesByBookId
    @id int
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
	where Book = @id;

END
Go