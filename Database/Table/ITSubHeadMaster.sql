USE [LitigationApp]
GO

ALTER TABLE [dbo].[ITSubHeadMaster] DROP CONSTRAINT [FK_ITSubHeadMaster_ITHeadMaster_Id]
GO

/****** Object:  Table [dbo].[ITSubHeadMaster]    Script Date: 6/26/2018 10:53:22 AM ******/
DROP TABLE [dbo].[ITSubHeadMaster]
GO

/****** Object:  Table [dbo].[ITSubHeadMaster]    Script Date: 6/26/2018 10:53:22 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ITSubHeadMaster](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ITHeadId] [bigint] NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[IsAllowance] [bit] NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ITSubHeadMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ITSubHeadMaster]  WITH CHECK ADD  CONSTRAINT [FK_ITSubHeadMaster_ITHeadMaster_Id] FOREIGN KEY([ITHeadId])
REFERENCES [dbo].[ITHeadMaster] ([Id])
GO

ALTER TABLE [dbo].[ITSubHeadMaster] CHECK CONSTRAINT [FK_ITSubHeadMaster_ITHeadMaster_Id]
GO


