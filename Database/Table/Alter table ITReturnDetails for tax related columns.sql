USE [LitigationApp]
GO

ALTER TABLE [ITReturnDetails]
ADD TaxProvisions [decimal](18, 2) NULL
GO

ALTER TABLE [ITReturnDetails]
ADD TaxAssets [decimal](18, 2) NULL
GO

ALTER TABLE [ITReturnDetails]
ADD ContingentLiabilities [decimal](18, 2) NULL
GO

ALTER TABLE [ITReturnDetails]
ADD ImplementorId [BIGINT] NULL
GO

ALTER TABLE [dbo].[ITReturnDetails]  WITH CHECK ADD  CONSTRAINT [FK_ITReturnDetails_ImplementorMaster_Id] 
FOREIGN KEY([ImplementorId])
REFERENCES [dbo].[ImplementorMaster] ([Id])
GO

ALTER TABLE [dbo].[ITReturnDetails] CHECK CONSTRAINT [FK_ITReturnDetails_ImplementorMaster_Id]
GO