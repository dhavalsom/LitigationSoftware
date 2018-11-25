USE [LitigationApp]
GO

ALTER TABLE [dbo].[LAndSComments] DROP CONSTRAINT [FK_LAndSComments_ITSubHeadMaster_Id]
GO

ALTER TABLE [dbo].[LAndSComments] DROP CONSTRAINT [FK_LAndSComments_CompanyMaster_Id]
GO

/****** Object:  Table [dbo].[LAndSComments]    Script Date: 11/25/2018 11:25:25 AM ******/
DROP TABLE [dbo].[LAndSComments]
GO

/****** Object:  Table [dbo].[LAndSComments]    Script Date: 11/25/2018 11:25:25 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LAndSComments](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[ITSubHeadId] [bigint] NOT NULL,
	[Comment] [nvarchar](1000) NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_LAndSComments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[LAndSComments]  WITH CHECK ADD  CONSTRAINT [FK_LAndSComments_CompanyMaster_Id] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[CompanyMaster] ([Id])
GO

ALTER TABLE [dbo].[LAndSComments] CHECK CONSTRAINT [FK_LAndSComments_CompanyMaster_Id]
GO

ALTER TABLE [dbo].[LAndSComments]  WITH CHECK ADD  CONSTRAINT [FK_LAndSComments_ITSubHeadMaster_Id] FOREIGN KEY([ITSubHeadId])
REFERENCES [dbo].[ITSubHeadMaster] ([Id])
GO

ALTER TABLE [dbo].[LAndSComments] CHECK CONSTRAINT [FK_LAndSComments_ITSubHeadMaster_Id]
GO


