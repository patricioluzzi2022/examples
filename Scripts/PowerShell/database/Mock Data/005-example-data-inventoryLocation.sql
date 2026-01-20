USE [data_analysis_book]
GO

INSERT INTO [dbo].[InventoryLocation]
           ([Book]
           ,[Shelf]
           ,[Code]
           ,[RowNumber]
           ,[ColumnNumber]
           ,[Branch]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           (1
           ,'Giroscope'
           ,'54-00-0343-0002' -- tt-clcl--wwwww-####
           ,-30.1985102
           ,-60.1955110
           ,'54'
           ,'2025-04-15'
           ,getdate());
Go

--INSERT INTO [dbo].[InventoryLocation]
--           ([Book]
--           ,[Shelf]
--           ,[Code]
--           ,[RowNumber]
--           ,[ColumnNumber]
--           ,[Branch]
--           ,[CreationDate]
--           ,[ModificationDate])
--     VALUES
--           (1
--           ,'Giroscope'
--           ,'54-00-0343-0002' -- tt-clcl--wwwww-####
--           ,-30.1985102
--           ,-60.1955110
--           ,'54'
--           ,'2025-04-15'
--           ,getdate());
--Go
--
--INSERT INTO [dbo].[InventoryLocation]
--           ([Book]
--           ,[Shelf]
--           ,[Code]
--           ,[RowNumber]
--           ,[ColumnNumber]
--           ,[Branch]
--           ,[CreationDate]
--           ,[ModificationDate])
--     VALUES
--           (3
--           ,'Giroscope'
--           ,'54-00-0343-0001' -- tt-clcl--wwwww-####
--           ,0
--           ,0
--           ,''
--           ,'2025-09-26'
--           ,getdate());
--Go
--
--INSERT INTO [dbo].[InventoryLocation]
--           ([Book]
--           ,[Shelf]
--           ,[Code]
--           ,[RowNumber]
--           ,[ColumnNumber]
--           ,[Branch]
--           ,[CreationDate]
--           ,[ModificationDate])
--     VALUES
--           (4
--           ,'Giroscope'
--           ,'54-00-0343-0001' -- tt-clcl--wwwww-####
--           ,0
--           ,0
--           ,''
--           ,'2025-09-26'
--           ,getdate());
--Go
--
--INSERT INTO [dbo].[InventoryLocation]
--           ([Book]
--           ,[Shelf]
--           ,[Code]
--           ,[RowNumber]
--           ,[ColumnNumber]
--           ,[Branch]
--           ,[CreationDate]
--           ,[ModificationDate])
--     VALUES
--           (5
--           ,'Giroscope'
--           ,'54-00-0343-0001' -- tt-clcl--wwwww-####
--           ,0
--           ,0
--           ,''
--           ,'2025-09-26'
--           ,getdate());
--Go
--
--INSERT INTO [dbo].[InventoryLocation]
--           ([Book]
--           ,[Shelf]
--           ,[Code]
--           ,[RowNumber]
--           ,[ColumnNumber]
--           ,[Branch]
--           ,[CreationDate]
--           ,[ModificationDate])
--     VALUES
--           (6
--           ,'Giroscope'
--           ,'54-00-0343-0001' -- tt-clcl--wwwww-####
--           ,0
--           ,0
--           ,''
--           ,'2025-09-26'
--           ,getdate());
--Go
--
--INSERT INTO [dbo].[InventoryLocation]
--           ([Book]
--           ,[Shelf]
--           ,[Code]
--           ,[RowNumber]
--           ,[ColumnNumber]
--           ,[Branch]
--           ,[CreationDate]
--           ,[ModificationDate])
--     VALUES
--           (7
--           ,'Giroscope'
--           ,'54-00-0343-0001' -- tt-clcl--wwwww-####
--           ,0
--           ,0
--           ,''
--           ,'2025-09-26'
--           ,getdate());
--Go