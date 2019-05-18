USE [LitigationApp]
GO

ALTER TABLE [dbo].[CompetitorTaxRate] DROP CONSTRAINT [FK_CompetitorTaxRate_FYAYMaster_Id]
GO

ALTER TABLE [dbo].[CompetitorTaxRate] DROP CONSTRAINT [FK_CompetitorTaxRate_CompetitorMaster_Id]
GO

/****** Object:  Table [dbo].[CompetitorTaxRate]    Script Date: 5/2/2019 11:58:54 PM ******/
DROP TABLE [dbo].[CompetitorTaxRate]
GO

/****** Object:  Table [dbo].[CompetitorTaxRate]    Script Date: 5/2/2019 11:58:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CompetitorTaxRate](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CompetitorId] [bigint] NOT NULL,
	[FYAYId] [bigint] NOT NULL,
	[TaxRate] [decimal](18, 2) NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_CompetitorTaxRate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CompetitorTaxRate]  WITH CHECK ADD  CONSTRAINT [FK_CompetitorTaxRate_CompetitorMaster_Id] FOREIGN KEY([CompetitorId])
REFERENCES [dbo].[CompetitorMaster] ([Id])
GO

ALTER TABLE [dbo].[CompetitorTaxRate] CHECK CONSTRAINT [FK_CompetitorTaxRate_CompetitorMaster_Id]
GO

ALTER TABLE [dbo].[CompetitorTaxRate]  WITH CHECK ADD  CONSTRAINT [FK_CompetitorTaxRate_FYAYMaster_Id] FOREIGN KEY([FYAYId])
REFERENCES [dbo].[FYAYMaster] ([Id])
GO

ALTER TABLE [dbo].[CompetitorTaxRate] CHECK CONSTRAINT [FK_CompetitorTaxRate_FYAYMaster_Id]
GO


