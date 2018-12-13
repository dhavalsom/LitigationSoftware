ALTER TABLE ITHeadMaster
ADD IsSpecialIncomeEnabled BIT NOT NULL DEFAULT(0)

GO

UPDATE ITHeadMaster
SET IsSpecialIncomeEnabled = 1
WHERE PropertyName IN
('IncomefromCapGainsLTCG','IncomefromCapGainsSTCG','IncomeFromOtherSources','SelfassessmentTax','RegularAssessment')
GO