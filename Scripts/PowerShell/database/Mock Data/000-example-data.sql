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
           ,'1988-11-16'
           ,'2025-04-07'
           ,getdate());
Go

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
           ,'54-00-0343-0001' -- tt-clcl--wwwww-####
           ,0 --row
           ,0 --colimn
           ,''
           ,'2025-09-26' --creation date
           ,getdate());
Go

INSERT INTO [dbo].[Notes]
           ([Book]
           ,[TheMessage]
           ,[Nickname]
           ,[Page]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           (1
           ,'Finding an old new start '
           ,'Uwonme'
           ,54
           ,getdate()
           ,getdate());
GO

INSERT INTO [dbo].[Books]
           ([Author]
           ,[Title]
           ,[Description]
           ,[Publisher]
           ,[Edition]
           ,[Stock]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           (1
            ,'Can I believe ' --title
           ,'One of the best books I have read in my life. It is a book that makes you think about your life and the world around you. It is a book that makes you feel alive.' --description
           ,1 --publisher
           ,'1st Edition' --edition
           ,4 --stock
           ,'1986-02-22' --creation date
           ,getdate())
GO

INSERT INTO [dbo].[Notes]
           ([Book]
           ,[TheMessage]
           ,[Nickname]
           ,[Page]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           (1
           ,'Something that I have to do'
           ,'DayLikeThis'
           ,337
           ,getdate()
           ,getdate());
GO

INSERT INTO [dbo].[Publishers]
           ([Name]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           ('PS: Intestingland Publishing'
           ,getdate()
           ,getdate());
GO

INSERT INTO [dbo].[Notes]
           ([Book]
           ,[TheMessage]
           ,[Nickname]
           ,[Page]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           (1
           ,'Something that I have to do'
           ,'DayLikeThis'
           ,543
           ,getdate()
           ,getdate());
GO

INSERT INTO [dbo].[Books]
           ([Author]
           ,[Title]
           ,[Description]
           ,[Publisher]
           ,[Edition]
           ,[Stock]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           (0
           ,'I can see our reflection in them' --title
           ,'''I can''t hate what we are no longer, but we could have been.''' --description
           ,1 --publisher
           ,'3st Edition' --edition
           ,0 --stock
           ,'2024-11-17' --creation date
           ,getdate());
GO

INSERT INTO [dbo].[Notes]
           ([Book]
           ,[TheMessage]
           ,[Nickname]
           ,[Page]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           (1
           ,'Maybe my 4 was my 6, maybe my 7 had to remind me that it added up to 10 and not 7. Maybe it wasn''t our time.'
           ,'DoubleCheck'
           ,231
           ,getdate()
           ,getdate());
GO

INSERT INTO [dbo].[Notes]
           ([Book]
           ,[TheMessage]
           ,[Nickname]
           ,[Page]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           (1
           ,'Descasar ayuda a recordar lo que no se ha olvidado'
           ,'CucharaDeMadera'
           ,237
           ,getdate()
           ,getdate());
GO

INSERT INTO [dbo].[Notes]
           ([Book]
           ,[TheMessage]
           ,[Nickname]
           ,[Page]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           (1
           ,'Hojas de otoño'
           ,'Añorando una brisa, un silencio, una auscencia; Añorando la memoria de un pasado quizas noñe.'
           ,40
           ,getdate()
           ,getdate());
GO

INSERT INTO [dbo].[Books]
           ([Author]
           ,[Title]
           ,[Description]
           ,[Publisher]
           ,[Edition]
           ,[Stock]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           (5
           ,'Siguiendo la musica' --title
           ,'''The day your gone, you said: follow the music.''' --description
           ,1 --publisher
           ,'1st Edition' --edition
           ,0 --stock
           ,getdate() --creation date
           ,getdate());
GO
INSERT INTO [dbo].[Notes]
           ([Book]
           ,[TheMessage]
           ,[Nickname]
           ,[Page]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           (5
           ,'Juan Luis Guerra'
           ,'El Ñagara en bicicleta.'
           ,33
           ,getdate()
           ,getdate());
GO


INSERT INTO [dbo].[Notes]
           ([Book]
           ,[TheMessage]
           ,[Nickname]
           ,[Page]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           (5
           ,'Ciro y los Persas'
           ,'Antes & Despues'
           ,3581
           ,getdate()
           ,getdate());
GO

INSERT INTO [dbo].[Notes]
           ([Book]
           ,[TheMessage]
           ,[Nickname]
           ,[Page]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           (5
           ,'Moving on'
           ,'Where is the wooden spoon?'
           ,873
           ,getdate()
           ,getdate());
GO

INSERT INTO [dbo].[Notes]
           ([Book]
           ,[TheMessage]
           ,[Nickname]
           ,[Page]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           (5
           ,'Agua saborizada'
           ,'Donde puedo conseguir un agua saborizada de pera?'
           ,842
           ,getdate()
           ,getdate());
GO

INSERT INTO [dbo].[Notes]
           ([Book]
           ,[TheMessage]
           ,[Nickname]
           ,[Page]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           (5
           ,'Sugar weather'
           ,'Tracksuit and Footwear'
           ,222
           ,getdate()
           ,getdate());
GO

INSERT INTO [dbo].[Notes]
           ([Book]
           ,[TheMessage]
           ,[Nickname]
           ,[Page]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           (6
           ,'Sugar weather'
           ,'Si todos mienten, decir la verdad no hace diferencia '
           ,1143
           ,getdate()
           ,getdate());
GO

INSERT INTO [dbo].[Notes]
           ([Book]
           ,[TheMessage]
           ,[Nickname]
           ,[Page]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           (6
           ,'Sugar weather'
           ,'Si todos mienten, decir la verdad no hace diferencia '
           ,1143
           ,getdate()
           ,getdate());
GO

INSERT INTO [dbo].[Notes]
           ([Book]
           ,[TheMessage]
           ,[Nickname]
           ,[Page]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           (6
           ,'Fun and curious mathematics, geometric representation of real numbers.'
           ,'ReneFrogger'
           ,21
           ,getdate()
           ,getdate());
GO

INSERT INTO [dbo].[Notes]
           ([Book]
           ,[TheMessage]
           ,[Nickname]
           ,[Page]
           ,[CreationDate]
           ,[ModificationDate])
     VALUES
           (6
           ,' 2D modeling'
           ,'LeonnardFlower'
           ,1143
           ,getdate()
           ,getdate());
GO