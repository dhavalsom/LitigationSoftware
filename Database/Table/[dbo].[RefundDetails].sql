USE [LitigationApp]
GO

ALTER TABLE [dbo].[RefundDetails] DROP CONSTRAINT [FK_RefundDetails_ITReturnDetailsID]
GO

ALTER TABLE [dbo].[RefundDetails] DROP CONSTRAINT [FK_RefundDetails_ITHeadMasterID]
GO

ALTER TABLE [dbo].[RefundDetails] DROP CONSTRAINT [FK_RefundDetails_FYAYID]
GO

/****** Object:  Table [dbo].[RefundDetails]    Script Date: 3/10/2019 1:33:21 PM ******/
DROP TABLE [dbo].[RefundDetails]
GO

/****** Object:  Table [dbo].[RefundDetails]    Script Date: 3/10/2019 1:33:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RefundDetails](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ITReturnDetailsID] [bigint] NOT NULL,
	[ITHeadMasterID] [bigint] NOT NULL,
	[FYAYID] [bigint] NOT NULL,
	[RefAmount] [decimal](18, 2) NOT NULL,
	[RefDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_RefundDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[RefundDetails]  WITH CHECK ADD  CONSTRAINT [FK_RefundDetails_FYAYID] FOREIGN KEY([FYAYID])
REFERENCES [dbo].[FYAYMaster] ([Id])
GO

ALTER TABLE [dbo].[RefundDetails] CHECK CONSTRAINT [FK_RefundDetails_FYAYID]
GO

ALTER TABLE [dbo].[RefundDetails]  WITH CHECK ADD  CONSTRAINT [FK_RefundDetails_ITHeadMasterID] FOREIGN KEY([ITHeadMasterID])
REFERENCES [dbo].[ITHeadMaster] ([Id])
GO

ALTER TABLE [dbo].[RefundDetails] CHECK CONSTRAINT [FK_RefundDetails_ITHeadMasterID]
GO

ALTER TABLE [dbo].[RefundDetails]  WITH CHECK ADD  CONSTRAINT [FK_RefundDetails_ITReturnDetailsID] FOREIGN KEY([ITReturnDetailsID])
REFERENCES [dbo].[ITReturnDetails] ([Id])
GO

ALTER TABLE [dbo].[RefundDetails] CHECK CONSTRAINT [FK_RefundDetails_ITReturnDetailsID]
GO


