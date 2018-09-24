ALTER TABLE ITHeadMaster
ADD CanAddDocuments BIT NULL
GO

UPDATE ITHeadMaster
SET CanAddDocuments = 0

GO

UPDATE ITHeadMaster
SET CanAddDocuments = 1
WHERE ExcelSrNo IN (13,14,20)
GO
