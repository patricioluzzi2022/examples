USE [data_analysis_book];
GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'GetAuthorsByFilters') AND type = N'P')
BEGIN
    DROP PROCEDURE [GetAuthorsByFilters];
END
GO

--------------------------------------------------
-- Stored Procedure GetAuthorsByFilters ----------
-- Description: Get the Authors information ------
-- fields filtering by the publisher, autho and --
-- the book --------------------------------------
CREATE PROCEDURE GetAuthorsByFilters
    @PublisherId INT = null,
    @AuthorId INT = null,
    @BookId INT = null
AS
BEGIN
    SELECT a.Id, a.FirstName, a.LastName, a.Nationality, a.BirthDate, a.CreationDate, a.ModificationDate
    FROM Authors a
    left join Books b on b.Author = a.Id
    left join Publishers p on b.Publisher = p.Id
    WHERE b.Publisher = isnull(@PublisherId, b.Publisher)
    AND a.Id = isnull(@AuthorId, a.Id)
    AND b.Id = isnull(@BookId, b.Id);
END
Go