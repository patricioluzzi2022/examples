USE [data_analysis_book];
Go

--------------------------------------------------
-- Function GetNoteId ----------------------------
-- Description: Get the note information fields --
-- filtering by the code and the variant of the --
-- code. ----------------------------------------
CREATE FUNCTION GetNoteId
(
    @code NVARCHAR(365),
    @full_code INT = 4
)
RETURNS INT
AS
BEGIN
	DECLARE @value varchar(50);

	with codes as (
		select value, ROW_NUMBER() over(order by (select 1)) as position from string_split(@code, '-')
	)
	select @value = value
	from codes
	where position = @full_code

	RETURN @value
END
GO