USE [data_analysis_book];
GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'GetNotes') AND type = N'P')
BEGIN
    DROP PROCEDURE [GetNotes];
END
GO

CREATE PROCEDURE GetNotes
    @id INT = null
AS
BEGIN
	set @id = 1;

    with ps as (
        select
			n.Id as Id,
			1 as il,
			b.Title as Title,
			CONCAT(a.FirstName, ' ', a.LastName, ' from ', a.Nationality) as Author,
			b.Edition as Edition,
			n.TheMessage as 'Note',
			n.Nickname as 'Note author',
			n.Page as Page
		from Notes n
		left join Books b on b.Id = n.Book
		left join Authors a on a.Id = b.Author
		left join InventoryLocation il on il.Book = n.Book
        where dbo.GetNoteId(il.Code, 1) =
		   case
				when dbo.GetNoteId(il.Code, 1) = 1 then 1
		        else 0
		   end
	)
	select
	*
	from ps
	where Id = @id

END
Go