USE [data_analysis_book]
GO

INSERT INTO [dbo].[Publishers]
           ([Name]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           ('Intestingland Publishing'
           ,getdate()
           ,getdate());
GO

INSERT INTO [dbo].[Publishers]
           ([Name]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           ('Wonderland Publishing'
           ,getdate()
           ,getdate());
GO

INSERT INTO [dbo].[Publishers]
           ([Name]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           ('Nevermine Publishing'
           ,getdate()
           ,getdate());
GO