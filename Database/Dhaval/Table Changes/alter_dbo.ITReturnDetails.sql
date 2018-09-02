use LitigationApp

go

EXEC sp_RENAME 'dbo.ITReturnDetails.IncomefromCapGainsNonSTT' , 'IncomefromCapGainsLTCG', 'COLUMN'
alter table dbo.ITReturnDetails alter column IncomefromCapGainsLTCG DECIMAL(18,2) 

go

EXEC sp_RENAME 'dbo.ITReturnDetails.IncomefromCapGainsSTT' , 'IncomefromCapGainsSTCG', 'COLUMN'
alter table dbo.ITReturnDetails alter column IncomefromCapGainsSTCG DECIMAL(18,2) 

go

EXEC sp_RENAME 'dbo.ITReturnDetails.UnabsorbedDepreciation' , 'IncomefromSpeculativeBusiness', 'COLUMN'
alter table dbo.ITReturnDetails alter column IncomefromSpeculativeBusiness DECIMAL(18,2) 

go

alter table dbo.ITReturnDetails alter column HousePropIncome DECIMAL(18,2)
alter table dbo.ITReturnDetails alter column incomefrombusinessprof DECIMAL(18,2) 
alter table dbo.ITReturnDetails alter column IncomeFromOtherSources DECIMAL(18,2)
alter table dbo.ITReturnDetails alter column Broughtforwardlosses bit
alter table dbo.ITReturnDetails alter column DeductChapterVIA DECIMAL(18,2)
alter table dbo.ITReturnDetails alter column ProfitUS115JB DECIMAL(18,2)
alter table dbo.ITReturnDetails alter column AdvanceTax1installment  DECIMAL(18,2)
alter table dbo.ITReturnDetails alter column AdvanceTax2installment  DECIMAL(18,2)
alter table dbo.ITReturnDetails alter column AdvanceTax3installment  DECIMAL(18,2)
alter table dbo.ITReturnDetails alter column AdvanceTax4installment  DECIMAL(18,2)
alter table dbo.ITReturnDetails alter column TDS DECIMAL(18,2)
alter table dbo.ITReturnDetails alter column TCSPaidbyCompany DECIMAL(18,2)
alter table dbo.ITReturnDetails alter column SelfassessmentTax DECIMAL(18,2)
alter table dbo.ITReturnDetails alter column MATCredit DECIMAL(18,2)
alter table dbo.ITReturnDetails alter column InterestUS234A DECIMAL(18,2)
alter table dbo.ITReturnDetails alter column InterestUS234B DECIMAL(18,2)
alter table dbo.ITReturnDetails alter column InterestUS234C DECIMAL(18,2)

go

alter table dbo.ITReturnDetails add IncomefromSalary DECIMAL(18,2)
alter table dbo.ITReturnDetails add TDS26AS DECIMAL(18,2)
alter table dbo.ITReturnDetails add TDSasperBooks DECIMAL(18,2)



