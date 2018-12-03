ALTER TABLE ITReturnDetails  
ADD
TaxCollectedAtSource [decimal](18, 2) NULL,
ForeignTaxCredit [decimal](18, 2) NULL,
InterestUS234D [decimal](18, 2) NULL,
InterestUS220 [decimal](18, 2) NULL,
RefundAdjusted [decimal](18, 2) NULL,
RegularAssessment [decimal](18, 2) NULL,
SelfAssessmentTaxDate [datetime] NULL,
AdvanceTax1installmentDate [datetime] NULL,
AdvanceTax2installmentDate [datetime] NULL,
AdvanceTax3installmentDate [datetime] NULL,
AdvanceTax4installmentDate [datetime] NULL,
RefundAdjustedDate [datetime] NULL,
RegularAssessmentDate [datetime] NULL
GO

/*
TaxCollectedAtSource
ForeignTaxCredit
InterestUS234D
InterestUS220
RefundAdjusted
RegularAssessment
SelfAssessmentTaxDate
AdvanceTax1installmentDate
AdvanceTax2installmentDate
AdvanceTax3installmentDate
AdvanceTax4installmentDate
RefundAdjustedDate
RegularAssessmentDate
*/