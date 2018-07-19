USE [LitigationApp]
GO

ALTER TABLE [dbo].[ITReturnDetails] DROP CONSTRAINT [FK_ITReturnDetails_ITSectionID]
GO

ALTER TABLE [dbo].[ITReturnDetails] DROP CONSTRAINT [FK_ITReturnDetails_FYAYID]
GO

ALTER TABLE [dbo].[ITReturnDetails] DROP CONSTRAINT [FK_ITReturnDetails_CompanyID]
GO

/****** Object:  Table [dbo].[ITReturnDetails]    Script Date: 7/15/2018 2:26:34 PM ******/
DROP TABLE [dbo].[ITReturnDetails]
GO

/****** Object:  Table [dbo].[ITReturnDetails]    Script Date: 7/15/2018 2:26:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ITReturnDetails](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyID] [bigint] NOT NULL,
	[FYAYID] [bigint] NOT NULL,
	[ITSectionID] [bigint] NOT NULL,
	[ITReturnFillingDate] [datetime] NULL,
	[ITReturnDueDate] [datetime] NULL,
	[HousePropIncome] [decimal](18, 2) NULL,
	[IncomefromCapGainsNonSTT] [decimal](18, 2) NULL,
	[IncomefromCapGainsSTT] [decimal](18, 2) NULL,
	[IncomefromBusinessProf] [bit] NULL,
	[UnabsorbedDepreciation] [decimal](18, 2) NULL,
	[Broughtforwardlosses] [decimal](18, 2) NULL,
	[IncomeFromOtherSources] [decimal](18, 2) NULL,
	[DeductChapterVIA] [decimal](18, 2) NULL,
	[ProfitUS115JB] [decimal](18, 2) NULL,
	[AdvanceTax1installment] [decimal](18, 2) NULL,
	[AdvanceTax2installment] [decimal](18, 2) NULL,
	[AdvanceTax3installment] [decimal](18, 2) NULL,
	[AdvanceTax4installment] [decimal](18, 2) NULL,
	[TDS] [decimal](18, 2) NULL,
	[TCSPaidbyCompany] [decimal](18, 2) NULL,
	[SelfassessmentTax] [decimal](18, 2) NULL,
	[MATCredit] [decimal](18, 2) NULL,
	[InterestUS234A] [decimal](18, 2) NULL,
	[InterestUS234B] [decimal](18, 2) NULL,
	[InterestUS234C] [decimal](18, 2) NULL,
	[InterestUS244A] [decimal](18, 2) NULL,
	[RefundReceived] [decimal](18, 2) NULL,
	[RevisedReturnFile] [bit] NULL,
	[IsDefault] [bit] NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ITReturnDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ITReturnDetails]  WITH CHECK ADD  CONSTRAINT [FK_ITReturnDetails_CompanyID] FOREIGN KEY([CompanyID])
REFERENCES [dbo].[CompanyMaster] ([Id])
GO

ALTER TABLE [dbo].[ITReturnDetails] CHECK CONSTRAINT [FK_ITReturnDetails_CompanyID]
GO

ALTER TABLE [dbo].[ITReturnDetails]  WITH CHECK ADD  CONSTRAINT [FK_ITReturnDetails_FYAYID] FOREIGN KEY([FYAYID])
REFERENCES [dbo].[FYAYMaster] ([Id])
GO

ALTER TABLE [dbo].[ITReturnDetails] CHECK CONSTRAINT [FK_ITReturnDetails_FYAYID]
GO

ALTER TABLE [dbo].[ITReturnDetails]  WITH CHECK ADD  CONSTRAINT [FK_ITReturnDetails_ITSectionID] FOREIGN KEY([ITSectionID])
REFERENCES [dbo].[ITSectionMaster] ([Id])
GO

ALTER TABLE [dbo].[ITReturnDetails] CHECK CONSTRAINT [FK_ITReturnDetails_ITSectionID]
GO


