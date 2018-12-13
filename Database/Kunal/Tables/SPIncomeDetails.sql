USE [LitigationApp]
GO

ALTER TABLE [dbo].[SPIncomeDetails] DROP CONSTRAINT [FK_SPIncomeDetails_ITReturnDetails_Id]
GO

ALTER TABLE [dbo].[SPIncomeDetails] DROP CONSTRAINT [FK_SPIncomeDetails_ITHeadMaster_Id]
GO

/****** Object:  Table [dbo].[SPIncomeDetails]    Script Date: 12/8/2018 11:40:28 AM ******/
DROP TABLE [dbo].[SPIncomeDetails]
GO

/****** Object:  Table [dbo].[SPIncomeDetails]    Script Date: 12/8/2018 11:40:28 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SPIncomeDetails](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ITReturnDetailsId] [bigint] NOT NULL,
	[ITHeadId] [bigint] NOT NULL,
	[SPIncomeDescription] [nvarchar](255) NULL,
	[SPIncomeValue] [decimal](18, 2) NULL,
	[TaxRate] [decimal](18, 2) NULL,
	[SPIncomeDate] [datetime] NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_SPIncomeDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SPIncomeDetails]  WITH CHECK ADD  CONSTRAINT [FK_SPIncomeDetails_ITHeadMaster_Id] FOREIGN KEY([ITHeadId])
REFERENCES [dbo].[ITHeadMaster] ([Id])
GO

ALTER TABLE [dbo].[SPIncomeDetails] CHECK CONSTRAINT [FK_SPIncomeDetails_ITHeadMaster_Id]
GO

ALTER TABLE [dbo].[SPIncomeDetails]  WITH CHECK ADD  CONSTRAINT [FK_SPIncomeDetails_ITReturnDetails_Id] FOREIGN KEY([ITReturnDetailsId])
REFERENCES [dbo].[ITReturnDetails] ([Id])
GO

ALTER TABLE [dbo].[SPIncomeDetails] CHECK CONSTRAINT [FK_SPIncomeDetails_ITReturnDetails_Id]
GO


