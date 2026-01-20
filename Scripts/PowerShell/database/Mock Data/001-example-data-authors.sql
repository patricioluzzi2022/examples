USE [data_analysis_book]
GO

INSERT INTO [dbo].[Authors]
           ([FirstName]
           ,[LastName]
           ,[Nationality]
           ,[BirthDate]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           ('Uwon'
           ,'Scope'
           ,'Interestingland'
           ,'1988-11-15'
           ,'2025-04-07'
           ,getdate());
Go

INSERT INTO [dbo].[Authors]
           ([FirstName]
           ,[LastName]
           ,[Nationality]
           ,[BirthDate]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           ('John'
           ,'Doe'
           ,'Wonderland'
           ,'1998-02-16'
           ,'2025-10-05'
           ,getdate());
Go

INSERT INTO [dbo].[Authors]
           ([FirstName]
           ,[LastName]
           ,[Nationality]
           ,[BirthDate]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           ('Pedro'
           ,'Bonifacio'
           ,'Enrnestland'
           ,'2013-09-25'
           ,'2025-04-15'
           ,getdate());
Go