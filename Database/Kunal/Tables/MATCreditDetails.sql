USE [LitigationApp]
GO

ALTER TABLE [dbo].[MATCreditDetails] DROP CONSTRAINT [FK_MATCreditDetails_ITSectionCategoryID]
GO

ALTER TABLE [dbo].[MATCreditDetails] DROP CONSTRAINT [FK_MATCreditDetails_FYAYID]
GO

ALTER TABLE [dbo].[MATCreditDetails] DROP CONSTRAINT [FK_MATCreditDetails_CompanyID]
GO

/****** Object:  Table [dbo].[MATCreditDetails]    Script Date: 12/30/2018 10:14:26 AM ******/
DROP TABLE [dbo].[MATCreditDetails]
GO

/****** Object:  Table [dbo].[MATCreditDetails]    Script Date: 12/30/2018 10:14:26 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MATCreditDetails](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[FYAYId] [bigint] NOT NULL,
	[ITSectionCategoryId] [bigint] NOT NULL,
	[BusinessLosses_BF] [decimal](18, 2) NULL,
	[UnabsorbedDepreciation_BF] [decimal](18, 2) NULL,
	[BusinessLosses_CY] [decimal](18, 2) NULL,
	[UnabsorbedDepreciation_CY] [decimal](18, 2) NULL,
	[BusinessLosses_UL] [decimal](18, 2) NULL,
	[UnabsorbedDepreciation_UL] [decimal](18, 2) NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_MATCreditDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UC_MATCreditDetails] UNIQUE NONCLUSTERED 
(
	[CompanyId] ASC,
	[FYAYId] ASC,
	[ITSectionCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MATCreditDetails]  WITH CHECK ADD  CONSTRAINT [FK_MATCreditDetails_CompanyID] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[CompanyMaster] ([Id])
GO

ALTER TABLE [dbo].[MATCreditDetails] CHECK CONSTRAINT [FK_MATCreditDetails_CompanyID]
GO

ALTER TABLE [dbo].[MATCreditDetails]  WITH CHECK ADD  CONSTRAINT [FK_MATCreditDetails_FYAYID] FOREIGN KEY([FYAYId])
REFERENCES [dbo].[FYAYMaster] ([Id])
GO

ALTER TABLE [dbo].[MATCreditDetails] CHECK CONSTRAINT [FK_MATCreditDetails_FYAYID]
GO

ALTER TABLE [dbo].[MATCreditDetails]  WITH CHECK ADD  CONSTRAINT [FK_MATCreditDetails_ITSectionCategoryID] FOREIGN KEY([ITSectionCategoryId])
REFERENCES [dbo].[ITSectionCategory] ([Id])
GO

ALTER TABLE [dbo].[MATCreditDetails] CHECK CONSTRAINT [FK_MATCreditDetails_ITSectionCategoryID]
GO


