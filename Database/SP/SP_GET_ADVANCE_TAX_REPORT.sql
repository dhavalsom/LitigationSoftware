USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_LOG_ERRORS]    Script Date: 6/09/2019 10:54:50 PM ******/
DROP PROCEDURE [dbo].[SP_GET_ADVANCE_TAX_REPORT]
GO

/****** Object:  StoredProcedure [dbo].[SP_LOG_ERRORS]    Script Date: 6/09/2019 10:54:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SP_GET_ADVANCE_TAX_REPORT
(	
	@COMPANY_ID bigint = NULL,
	@NO_OF_YEARS INT = 5
)
AS

BEGIN
		
	With QTR As (
		Select 'Q1' As Quarter Union All 
		Select 'Q2' Union All 
		Select 'Q3' Union All
		Select 'Q4'
	), FYAY As (
		Select FYM.Id As FYAYID, FYM.FinancialYear, Row_Number() Over(Order By FinancialYear Desc) As Row_Index 
		  From FYAYMaster FYM 
		 Where FYM.Active = 1
	), FYAY_QTR As (
		Select FYAYID, FinancialYear, QTR.Quarter, CAST(RAND(CHECKSUM(NEWID())) * 300 as INT) + 1 As AdvanceTax 
		 From FYAY 
		 Left Join QTR ON 1 = 1
		 Where Row_Index <= @NO_OF_YEARS
	)
	Select * From FYAY_QTR Order by FinancialYear, Quarter;

END

GO