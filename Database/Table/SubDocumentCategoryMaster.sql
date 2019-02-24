USE [LitigationApp]
GO

ALTER TABLE [dbo].[SubDocumentCategoryMaster] DROP CONSTRAINT [FK_SubDocumentCategoryMaster_DocumentCategoryMaster_Id]
GO

/****** Object:  Table [dbo].[SubDocumentCategoryMaster]    Script Date: 2/24/2019 12:25:39 AM ******/
DROP TABLE [dbo].[SubDocumentCategoryMaster]
GO

/****** Object:  Table [dbo].[SubDocumentCategoryMaster]    Script Date: 2/24/2019 12:25:39 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SubDocumentCategoryMaster](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DocumentCategoryId] [bigint] NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_SubDocumentCategoryMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SubDocumentCategoryMaster]  WITH CHECK ADD  CONSTRAINT [FK_SubDocumentCategoryMaster_DocumentCategoryMaster_Id] FOREIGN KEY([DocumentCategoryId])
REFERENCES [dbo].[DocumentCategoryMaster] ([Id])
GO

ALTER TABLE [dbo].[SubDocumentCategoryMaster] CHECK CONSTRAINT [FK_SubDocumentCategoryMaster_DocumentCategoryMaster_Id]
GO


