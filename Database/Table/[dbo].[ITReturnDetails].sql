USE LitigationApp
GO

/****** Object:  Table [dbo].[ITReturnDetails]    Script Date: 5/5/2018 7:52:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ITReturnDetails](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyID] [bigint] not null,
	[FYAYID] [bigint] not null,
	[ITSectionID] [bigint] not null,
	[ITReturnFillingDate] [datetime] null,
	[ITReturnDueDate] [datetime] null,
	[HousePropIncome] [bigint] null,
	[IncomefromCapGainsNonSTT] [bigint] null,
	[IncomefromCapGainsSTT] [bigint] null,
	[IncomefromBusinessProf] [bit] null,
	[UnabsorbedDepreciation] [bigint] null,
	[Broughtforwardlosses] [bigint] null,
	[IncomeFromOtherSources] [bigint] null,
	[DeductChapterVIA] [bigint] null,
	[ProfitUS115JB] [bigint] null,
	[AdvanceTax1installment] [bigint] null,
	[AdvanceTax2installment] [bigint] null,
	[AdvanceTax3installment] [bigint] null,
	[AdvanceTax4installment] [bigint] null,
	[TDS] [bigint] null,
	[TCSPaidbyCompany] [bigint] null,
	[SelfassessmentTax] [bit] null,
	[MATCredit] [bigint] null,
	[InterestUS234A] [bigint] null,
	[InterestUS234B] [bigint] null,
	[InterestUS234C] [bigint] null,
	[InterestUS244A] [bigint] null,
	[RefundReceived] [bigint] null,
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
