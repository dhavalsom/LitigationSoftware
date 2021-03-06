alter table [LitigationApp].[dbo].[ITHeadMaster] add [IsTaxComputed] bit not null default(0)

UPDATE [LitigationApp].[dbo].[ITHeadMaster]
SET IsTaxComputed = 1
WHERE PROPERTYNAME IN (
'AdvanceTax1installment','AdvanceTax2installment','AdvanceTax3installment','AdvanceTax4installment',
'TDS','TaxCollectedAtSource','MATCredit','InterestUS234A','InterestUS234B','InterestUS234C','InterestUS234D',
'InterestUS244A','InterestUS220','SelfAssessmentTax','RegularAssessment','RefundAdjusted','RefundAlreadyReceived'
)