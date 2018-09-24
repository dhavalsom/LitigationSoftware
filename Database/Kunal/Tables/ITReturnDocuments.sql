USE [LitigationApp]
GO

ALTER TABLE [dbo].[ITReturnDocuments] DROP CONSTRAINT [FK_ITReturnDocuments_ITReturnDetails_Id]
GO

ALTER TABLE [dbo].[ITReturnDocuments] DROP CONSTRAINT [FK_ITReturnDocuments_ITHeadMaster_Id]
GO

/****** Object:  Table [dbo].[ITReturnDocuments]    Script Date: 9/3/2018 10:46:59 PM ******/
DROP TABLE [dbo].[ITReturnDocuments]
GO

/****** Object:  Table [dbo].[ITReturnDocuments]    Script Date: 9/3/2018 10:46:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ITReturnDocuments](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ITReturnDetailsId] [bigint] NOT NULL,
	[ITHeadId] [bigint] NOT NULL,
	[FileName] [nvarchar](300) NULL,
	[FilePath] [nvarchar](max) NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ITReturnDocuments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[ITReturnDocuments]  WITH CHECK ADD  CONSTRAINT [FK_ITReturnDocuments_ITHeadMaster_Id] FOREIGN KEY([ITHeadId])
REFERENCES [dbo].[ITHeadMaster] ([Id])
GO

ALTER TABLE [dbo].[ITReturnDocuments] CHECK CONSTRAINT [FK_ITReturnDocuments_ITHeadMaster_Id]
GO

ALTER TABLE [dbo].[ITReturnDocuments]  WITH CHECK ADD  CONSTRAINT [FK_ITReturnDocuments_ITReturnDetails_Id] FOREIGN KEY([ITReturnDetailsId])
REFERENCES [dbo].[ITReturnDetails] ([Id])
GO

ALTER TABLE [dbo].[ITReturnDocuments] CHECK CONSTRAINT [FK_ITReturnDocuments_ITReturnDetails_Id]
GO


