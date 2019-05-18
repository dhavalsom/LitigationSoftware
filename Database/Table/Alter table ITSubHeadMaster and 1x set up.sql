USE LitigationApp
ALTER TABLE ITSubHeadMaster
ADD [SubHeadType] NVARCHAR(1) NULL
GO

UPDATE ITSubHeadMaster
SET [SubHeadType] = 'C'
GO
