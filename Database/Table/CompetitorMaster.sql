USE [LitigationApp]
GO

ALTER TABLE [dbo].[CompetitorMaster] DROP CONSTRAINT [FK_CompetitorMaster_CompanyMaster_Id]
GO

/****** Object:  Table [dbo].[CompetitorMaster]    Script Date: 5/2/2019 11:55:08 PM ******/
DROP TABLE [dbo].[CompetitorMaster]
GO

/****** Object:  Table [dbo].[CompetitorMaster]    Script Date: 5/2/2019 11:55:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CompetitorMaster](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_CompetitorMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CompetitorMaster]  WITH CHECK ADD  CONSTRAINT [FK_CompetitorMaster_CompanyMaster_Id] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[CompanyMaster] ([Id])
GO

ALTER TABLE [dbo].[CompetitorMaster] CHECK CONSTRAINT [FK_CompetitorMaster_CompanyMaster_Id]
GO


