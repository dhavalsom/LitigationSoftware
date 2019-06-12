USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_LOG_ERRORS]    Script Date: 6/09/2019 10:54:50 PM ******/
DROP PROCEDURE [dbo].[SP_GET_TAX_LIABILITY_REPORT]
GO

/****** Object:  StoredProcedure [dbo].[SP_LOG_ERRORS]    Script Date: 6/09/2019 10:54:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SP_GET_TAX_LIABILITY_REPORT
(	
	@COMPANY_ID bigint = NULL,
	@NO_OF_YEARS INT = 5
)
AS

BEGIN
		
	With TTN As (
		Select 'Advance Tax' As TaxTypeName Union All 
		Select 'Tax Deducted At Source' Union All 
		Select 'Foreign Tax Credit' Union All
		Select 'Self Assessment Tax'
	), FYAY As (
		Select FYM.Id As FYAYID, FYM.FinancialYear, Row_Number() Over(Order By FinancialYear Desc) As Row_Index 
		  From FYAYMaster FYM 
		 Where FYM.Active = 1
	), FYAY_QTR As (
		Select FYAYID, FinancialYear, TTN.TaxTypeName, CAST(RAND(CHECKSUM(NEWID())) * 10 as INT) + 1 As Tax 
		 From FYAY 
		 Left Join TTN ON 1 = 1
		 Where Row_Index <= @NO_OF_YEARS
	)
	Select * From FYAY_QTR Order by FinancialYear;

END

GO