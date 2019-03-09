USE [LitigationApp]
GO

INSERT INTO [LitigationApp].[dbo].[SubDocumentCategoryMaster] ([DocumentCategoryId],[Description],[Active],[AddedBy],[AddedDate])
     VALUES ('Permanent file',1,1,GETUTCDATE())
GO
INSERT INTO [LitigationApp].[dbo].[SubDocumentCategoryMaster] ([Description],[Active],[AddedBy],[AddedDate])
     VALUES ('ROI',1,1,GETUTCDATE())
GO
INSERT INTO [LitigationApp].[dbo].[SubDocumentCategoryMaster] ([Description],[Active],[AddedBy],[AddedDate])
     VALUES ('Assessment',1,1,GETUTCDATE())
GO
INSERT INTO [LitigationApp].[dbo].[SubDocumentCategoryMaster] ([Description],[Active],[AddedBy],[AddedDate])
     VALUES ('CIT(A)',1,1,GETUTCDATE())
GO
INSERT INTO [LitigationApp].[dbo].[SubDocumentCategoryMaster] ([Description],[Active],[AddedBy],[AddedDate])
     VALUES ('ITAT',1,1,GETUTCDATE())
GO
INSERT INTO [LitigationApp].[dbo].[SubDocumentCategoryMaster] ([Description],[Active],[AddedBy],[AddedDate])
     VALUES ('HC',1,1,GETUTCDATE())
GO
INSERT INTO [LitigationApp].[dbo].[SubDocumentCategoryMaster] ([Description],[Active],[AddedBy],[AddedDate])
     VALUES ('SC',1,1,GETUTCDATE())
GO


