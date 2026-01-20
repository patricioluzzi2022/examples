USE [data_analysis_book];
Go

--------------------------------------------------
-- Function GetInventoryLocationCode -------------
-- Description: Get the InventoryLocation code ---
---------------------------------------------------------
CREATE FUNCTION GetInventoryLocationCode
(
    @code NVARCHAR(365),
    @position INT = 4
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
	where position = @position

	RETURN @value
END
GO