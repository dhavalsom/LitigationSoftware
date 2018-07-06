USE [LitigationApp]
GO

ALTER TABLE [dbo].[ComplianceDocuments] DROP CONSTRAINT [FK_ComplianceDocuments_ITReturnDetails_Id]
GO

ALTER TABLE [dbo].[ComplianceDocuments] DROP CONSTRAINT [FK_ComplianceDocuments_ComplianceMaster_Id]
GO

/****** Object:  Table [dbo].[ComplianceDocuments]    Script Date: 7/5/2018 9:55:55 PM ******/
DROP TABLE [dbo].[ComplianceDocuments]
GO

/****** Object:  Table [dbo].[ComplianceDocuments]    Script Date: 7/5/2018 9:55:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ComplianceDocuments](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ComplianceId] [bigint] NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[FYAYId] [bigint] NOT NULL,
	[FileName] [nvarchar](300) NULL,
	[FilePath] [nvarchar](max) NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ComplianceDocuments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[ComplianceDocuments]  WITH CHECK ADD  CONSTRAINT [FK_ComplianceDocuments_ComplianceMaster_Id] FOREIGN KEY([ComplianceId])
REFERENCES [dbo].[ComplianceMaster] ([Id])
GO

ALTER TABLE [dbo].[ComplianceDocuments] CHECK CONSTRAINT [FK_ComplianceDocuments_ComplianceMaster_Id]
GO

ALTER TABLE [dbo].[ComplianceDocuments]  WITH CHECK ADD  CONSTRAINT [FK_ComplianceDocuments_CompanyMaster_Id] FOREIGN KEY([FYAYId])
REFERENCES [dbo].[CompanyMaster] ([Id])
GO

ALTER TABLE [dbo].[ComplianceDocuments] CHECK CONSTRAINT [FK_ComplianceDocuments_CompanyMaster_Id]
GO

ALTER TABLE [dbo].[ComplianceDocuments]  WITH CHECK ADD  CONSTRAINT [FK_ComplianceDocuments_FYAYMaster_Id] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[FYAYMaster] ([Id])
GO

ALTER TABLE [dbo].[ComplianceDocuments] CHECK CONSTRAINT [FK_ComplianceDocuments_FYAYMaster_Id]
GO

