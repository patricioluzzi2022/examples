USE [data_analysis_book]
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
           ,'Can I believe '
           ,'One of the best books I have read in my life. It is a book that makes you think about your life and the world around you. It is a book that makes you feel alive.' --description
           ,1
           ,'1st Edition'
           ,4
           ,'1986-02-22'
           ,getdate())
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
           ,'I can see our reflection in them'
           ,'''I can''t hate what we are no longer, but we could have been.'''
           ,3
           ,'3st Edition'
           ,0
           ,'2024-11-17'
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
           (10
           ,'Siguiendo la musica'
           ,'''The day your gone, you said: follow the music.'''
           ,2
           ,'3st Edition'
           ,0
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
           (10
           ,'Recetas de pastas'
           ,'''Ingredientes, preparaciones y mas.'''
           ,2
           ,'2st Edition'
           ,0
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
           (2
           ,'Mathematics, fun and curious.'
           ,'Numbers, geometric representations, cartesian coordinates, curves and trajectories.'
           ,3
           ,'4th Edition'
           ,1
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
           (2
           ,'2D part modeling.'
           ,'Tools for designing 2D parts, theory and practice.'
           ,3
           ,'Genesis 4.8 Edition'
           ,0
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
           (3
           ,'Cocodrilo Dundee - Bibliografia ilustrada.'
           ,'Historia de vida, relatos llenos de emosiones e ilustraciones que aparentan desafiar las leyes naturales.'
           ,3
           ,'Genesis 4.8 Edition'
           ,1
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
           (3
           ,'Seasson - numbers & colors.'
           ,'Friends - Backstage pass  .'
           ,6
           ,'Genesis 4.8 Edition'
           ,1
           ,getdate()
           ,getdate());
GO