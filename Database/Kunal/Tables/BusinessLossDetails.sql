USE [LitigationApp]
GO

ALTER TABLE [dbo].[BusinessLossDetails] DROP CONSTRAINT [FK_ITReturnDetails_ITSectionCategoryID]
GO

ALTER TABLE [dbo].[BusinessLossDetails] DROP CONSTRAINT [FK_BusinessLossDetails_FYAYID]
GO

ALTER TABLE [dbo].[BusinessLossDetails] DROP CONSTRAINT [FK_BusinessLossDetails_CompanyID]
GO

/****** Object:  Table [dbo].[BusinessLossDetails]    Script Date: 12/25/2018 9:07:58 AM ******/
DROP TABLE [dbo].[BusinessLossDetails]
GO

/****** Object:  Table [dbo].[BusinessLossDetails]    Script Date: 12/25/2018 9:07:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BusinessLossDetails](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[FYAYId] [bigint] NOT NULL,
	[ITSectionCategoryId] [bigint] NOT NULL,
	[IncomeCapGainsLTCG_BF] [decimal](18, 2) NULL,
	[IncomeCapGainsSTCG_BF] [decimal](18, 2) NULL,
	[IncomeBusinessProf_BF] [decimal](18, 2) NULL,
	[IncomeSpeculativeBusiness_BF] [decimal](18, 2) NULL,
	[UnabsorbedDepreciation_BF] [decimal](18, 2) NULL,
	[HousePropIncome_BF] [decimal](18, 2) NULL,
	[IncomeOtherSources_BF] [decimal](18, 2) NULL,
	[IncomeCapGainsLTCG_CY] [decimal](18, 2) NULL,
	[IncomeCapGainsSTCG_CY] [decimal](18, 2) NULL,
	[IncomeBusinessProf_CY] [decimal](18, 2) NULL,
	[IncomeSpeculativeBusiness_CY] [decimal](18, 2) NULL,
	[UnabsorbedDepreciation_CY] [decimal](18, 2) NULL,
	[HousePropIncome_CY] [decimal](18, 2) NULL,
	[IncomeOtherSources_CY] [decimal](18, 2) NULL,
	[IncomeCapGainsLTCG_UL] [decimal](18, 2) NULL,
	[IncomeCapGainsSTCG_UL] [decimal](18, 2) NULL,
	[IncomeBusinessProf_UL] [decimal](18, 2) NULL,
	[IncomeSpeculativeBusiness_UL] [decimal](18, 2) NULL,
	[UnabsorbedDepreciation_UL] [decimal](18, 2) NULL,
	[HousePropIncome_UL] [decimal](18, 2) NULL,
	[IncomeOtherSources_UL] [decimal](18, 2) NULL,
	[IncomeCapGainsLTCG_UALL] [decimal](18, 2) NULL,
	[IncomeCapGainsSTCG_UALL] [decimal](18, 2) NULL,
	[IncomeBusinessProf_UALL] [decimal](18, 2) NULL,
	[IncomeSpeculativeBusiness_UALL] [decimal](18, 2) NULL,
	[UnabsorbedDepreciation_UALL] [decimal](18, 2) NULL,
	[HousePropIncome_UALL] [decimal](18, 2) NULL,
	[IncomeOtherSources_UALL] [decimal](18, 2) NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_BusinessLossDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UC_BusinessLossDetails] UNIQUE NONCLUSTERED 
(
	[CompanyId] ASC,
	[FYAYId] ASC,
	[ITSectionCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[BusinessLossDetails]  WITH CHECK ADD  CONSTRAINT [FK_BusinessLossDetails_CompanyID] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[CompanyMaster] ([Id])
GO

ALTER TABLE [dbo].[BusinessLossDetails] CHECK CONSTRAINT [FK_BusinessLossDetails_CompanyID]
GO

ALTER TABLE [dbo].[BusinessLossDetails]  WITH CHECK ADD  CONSTRAINT [FK_BusinessLossDetails_FYAYID] FOREIGN KEY([FYAYId])
REFERENCES [dbo].[FYAYMaster] ([Id])
GO

ALTER TABLE [dbo].[BusinessLossDetails] CHECK CONSTRAINT [FK_BusinessLossDetails_FYAYID]
GO

ALTER TABLE [dbo].[BusinessLossDetails]  WITH CHECK ADD  CONSTRAINT [FK_ITReturnDetails_ITSectionCategoryID] FOREIGN KEY([ITSectionCategoryId])
REFERENCES [dbo].[ITSectionCategory] ([Id])
GO

ALTER TABLE [dbo].[BusinessLossDetails] CHECK CONSTRAINT [FK_ITReturnDetails_ITSectionCategoryID]
GO


