USE [data_analysis_book];
GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'GetNotesByTheMessage') AND type = N'P')
BEGIN
    DROP PROCEDURE [GetNotesByTheMessage];
END
GO

CREATE PROCEDURE GetNotesByTheMessage
    @theMessage nvarchar(max)
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
	where TheMessage like '%'+@theMessage+'%';

END
Go