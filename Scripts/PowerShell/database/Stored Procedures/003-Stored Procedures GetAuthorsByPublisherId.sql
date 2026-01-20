USE [data_analysis_book];
GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'GetAuthorsByPublisherId') AND type = N'P')
BEGIN
    DROP PROCEDURE [GetAuthorsByPublisherId];
END
GO

CREATE PROCEDURE GetAuthorsByPublisherId
    @id INT = null
AS
BEGIN
	select distinct
		a.Id,
		a.FirstName,
		a.LastName,
		a.Nationality,
		a.BirthDate,
		a.CreationDate,
		a.ModificationDate
	from Authors a
	left join Books b on a.Id = b.Author
	where b.Publisher = @id

END
Go