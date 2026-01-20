USE [data_analysis_book];
GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'GetShipmentsByCode') AND type = N'P')
BEGIN
    DROP PROCEDURE [GetShipmentsByCode];
END
GO

CREATE PROCEDURE GetShipmentsByCode
    @code nvarchar(50)
AS
BEGIN
	select
		Id,
		OriginLocations,
		DestinationLocations,
		Notes,
		Budget,
		Code,
		Stock,
		DeliveryDate,
		ReservationDate,
		Confirmation,
		CreationDate,
		ModificationDate
	from Shipments
	where Code = @code


END
Go
