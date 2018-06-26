USE [LitigationApp]
GO

ALTER TABLE [dbo].[ITReturnDetailsExtension] DROP CONSTRAINT [FK_ITReturnDetailsExtension_ITSubHeadMaster_Id]
GO

ALTER TABLE [dbo].[ITReturnDetailsExtension] DROP CONSTRAINT [FK_ITReturnDetailsExtension_ITReturnDetails_Id]
GO

/****** Object:  Table [dbo].[ITReturnDetailsExtension]    Script Date: 6/26/2018 10:58:25 AM ******/
DROP TABLE [dbo].[ITReturnDetailsExtension]
GO

/****** Object:  Table [dbo].[ITReturnDetailsExtension]    Script Date: 6/26/2018 10:58:25 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ITReturnDetailsExtension](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ITReturnDetailsId] [bigint] NOT NULL,
	[ITSubHeadId] [bigint] NOT NULL,
	[ITSubHeadValue] [decimal](18, 2) NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ITReturnDetailsExtension] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ITReturnDetailsExtension]  WITH CHECK ADD  CONSTRAINT [FK_ITReturnDetailsExtension_ITReturnDetails_Id] FOREIGN KEY([ITReturnDetailsId])
REFERENCES [dbo].[ITReturnDetails] ([Id])
GO

ALTER TABLE [dbo].[ITReturnDetailsExtension] CHECK CONSTRAINT [FK_ITReturnDetailsExtension_ITReturnDetails_Id]
GO

ALTER TABLE [dbo].[ITReturnDetailsExtension]  WITH CHECK ADD  CONSTRAINT [FK_ITReturnDetailsExtension_ITSubHeadMaster_Id] FOREIGN KEY([ITSubHeadId])
REFERENCES [dbo].[ITSubHeadMaster] ([Id])
GO

ALTER TABLE [dbo].[ITReturnDetailsExtension] CHECK CONSTRAINT [FK_ITReturnDetailsExtension_ITSubHeadMaster_Id]
GO


