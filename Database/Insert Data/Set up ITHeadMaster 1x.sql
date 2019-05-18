USE [LitigationApp]
GO

INSERT INTO [dbo].[ITHeadMaster]
           ([ExcelSrNo],[Description],[PropertyName]
           ,[CanAddSubHead],[Active],[AddedBy],[AddedDate]
           ,[CanAddDocuments],[IsSpecialIncomeEnabled],[IsROI],[HasDate],[IsTaxComputed])
     VALUES
           (35, 'Tax Provisions', 'TaxProvisions',
		   0, 1, 1, GETUTCDATE(), 0, 0, 1, 0, 0)

INSERT INTO [dbo].[ITHeadMaster]
           ([ExcelSrNo],[Description],[PropertyName]
           ,[CanAddSubHead],[Active],[AddedBy],[AddedDate]
           ,[CanAddDocuments],[IsSpecialIncomeEnabled],[IsROI],[HasDate],[IsTaxComputed])
     VALUES
           (36, 'Tax Assets', 'TaxAssets',
		   0, 1, 1, GETUTCDATE(), 0, 0, 1, 0, 0)
INSERT INTO [dbo].[ITHeadMaster]
           ([ExcelSrNo],[Description],[PropertyName]
           ,[CanAddSubHead],[Active],[AddedBy],[AddedDate]
           ,[CanAddDocuments],[IsSpecialIncomeEnabled],[IsROI],[HasDate],[IsTaxComputed])
     VALUES
           (37, 'Contingent Liabilities', 'ContingentLiabilities',
		   0, 1, 1, GETUTCDATE(), 0, 0, 1, 0, 0)