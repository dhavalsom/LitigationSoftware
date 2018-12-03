ALTER TABLE ITHeadMaster
ADD IsROI BIT NOT NULL DEFAULT(1)
GO

ALTER TABLE ITHeadMaster
ADD HasDate BIT NOT NULL DEFAULT(0)
GO

ALTER TABLE ITSubHeadMaster
ADD HasDate BIT NOT NULL DEFAULT(0)
GO

INSERT INTO [dbo].[ITHeadMaster]
           ([ExcelSrNo],[Description],[PropertyName],[CanAddSubHead],[Active],[AddedBy],[AddedDate],[CanAddDocuments],[IsROI],[HasDate])
     VALUES (23,'Tax Collected at Source','TaxCollectedAtSource',0,1,1,GETDATE(),0,1,0)
GO

UPDATE ITHeadMaster
SET ExcelSrNo = 24,
[Description] = 'Self-Assessment Tax paid by the company'
WHERE PropertyName = 'SelfAssessmentTax'
GO

INSERT INTO [dbo].[ITHeadMaster]
           ([ExcelSrNo],[Description],[PropertyName],[CanAddSubHead],[Active],[AddedBy],[AddedDate],[CanAddDocuments],[IsROI],[HasDate])
     VALUES (25,'Foreign Tax Credit','ForeignTaxCredit',0,1,1,GETDATE(),0,1,0)
GO

UPDATE ITHeadMaster
SET HasDate = 1
WHERE ExcelSrNo BETWEEN 15 AND 18
GO

INSERT INTO [dbo].[ITHeadMaster]
           ([Description],[PropertyName],[CanAddSubHead],[Active],[AddedBy],[AddedDate],[CanAddDocuments],[IsROI],[HasDate])
     VALUES ('Interest U/S 234D','InterestUS234D',0,1,1,GETDATE(),0,0,0)
GO

INSERT INTO [dbo].[ITHeadMaster]
           ([Description],[PropertyName],[CanAddSubHead],[Active],[AddedBy],[AddedDate],[CanAddDocuments],[IsROI],[HasDate])
     VALUES ('Interest U/S 244A','InterestUS244A',0,1,1,GETDATE(),0,0,0)
GO

INSERT INTO [dbo].[ITHeadMaster]
           ([Description],[PropertyName],[CanAddSubHead],[Active],[AddedBy],[AddedDate],[CanAddDocuments],[IsROI],[HasDate])
     VALUES ('Interest U/S 220','InterestUS220',0,1,1,GETDATE(),0,0,0)
GO

INSERT INTO [dbo].[ITHeadMaster]
           ([Description],[PropertyName],[CanAddSubHead],[Active],[AddedBy],[AddedDate],[CanAddDocuments],[IsROI],[HasDate])
     VALUES ('Refund Adjusted','RefundAdjusted',0,1,1,GETDATE(),0,0,1)
GO

INSERT INTO [dbo].[ITHeadMaster]
           ([Description],[PropertyName],[CanAddSubHead],[Active],[AddedBy],[AddedDate],[CanAddDocuments],[IsROI],[HasDate])
     VALUES ('Regular Assessment','RegularAssessment',0,1,1,GETDATE(),0,1,1)
GO