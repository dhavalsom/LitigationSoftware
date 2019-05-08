USE [LitigationApp]
GO

INSERT INTO [dbo].[ImplementorMaster]
           ([Description],[Active],[AddedBy],[AddedDate])
     VALUES
           ('Assessee', 1, 1, GETUTCDATE())
GO

INSERT INTO [dbo].[ImplementorMaster]
           ([Description],[Active],[AddedBy],[AddedDate])
     VALUES
           ('Department', 1, 1, GETUTCDATE())
GO

INSERT INTO [dbo].[ImplementorMaster]
           ([Description],[Active],[AddedBy],[AddedDate])
     VALUES
           ('Assessee/Department', 1, 1, GETUTCDATE())
GO


