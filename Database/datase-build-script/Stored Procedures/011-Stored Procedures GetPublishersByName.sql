USE [data_analysis_book];
GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'GetPublishersByName') AND type = N'P')
BEGIN
    DROP PROCEDURE [GetPublishersByName];
END
GO

CREATE PROCEDURE GetPublishersByName
    @name nvarchar(50) = null
AS
BEGIN
	select
		id,
		Code,
		Name,
		CreationDate,
		ModificationDate
	from Publishers
	where Name = @name;

END
Go
